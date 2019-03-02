const state = {
    language: "en",
    weddingDate: new Date("2019-08-02 16:00"),
    view: "rsvp", // welcome
    address: "A O Elliots Väg 10, 413 11 Göteborg",
    contact: {
        bride: {
            name: "Nathalie",
            phone: "555-000-124",
            email: "nathalie@worldwideweb.com"
        },
        groom: {
            name: "Karl",
            phone: "555-000-123",
            email: "karl@worldwideweb.com"
        }
    },
    rsvp: {
        firstName: "",
        lastName: "",
        email: "",
        phone: "",
        attendance: {
            wedding: null,
            cermony: null
        },
        food: {
            meat: null,
            fish: null,
            vegetarian: null,
            vegan: null
        },
        message: ""
    },
    formattedDate: () => {
        let months = [];
        if (state.language == "en") {
            months = [
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            ];
        } else {
            months = [
                "januari", "februari", "mars", "april", "maj", "juni",
                "juli", "augusti", "september", "oktober", "november", "december"
            ];
        }
        const day = state.weddingDate.getDate();
        const month = months[state.weddingDate.getMonth()];
        const year = state.weddingDate.getFullYear();
        return `${day} ${month}, ${year}`;
    },
    formattedTime: () => {
        const minutes = numberPad(state.weddingDate.getMinutes());
        const hours = numberPad(state.weddingDate.getHours());
        return `${hours}:${minutes}`;
    }
};

class DataBindingMode {
    constructor(isTwoWay) {
        this.isTwoWay = isTwoWay;
    }
    static TwoWay() {
        return new DataBindingMode(true);
    }
    static OneWay() {
        return new DataBindingMode(false);
    }
}

class DataBinding {
    constructor(element, attribute, binding, bindingMode) {
        this.element = element;
        this.attribute = attribute;
        this.binding = binding;
        this.bindingMode = bindingMode;
        this.evaluator = undefined;
        this.lastValue = undefined;
    }

    updateBinding(evaluator) {
        this.evaluator = evaluator;
        if (!this.element) {
            return;
        }
        if (!this.evaluator) {
            return;
        }
        this.evaluateBoundValue();
    }

    get value() {
        return this.element[this.attribute];
    }

    detectChanges() {
        // ugly hax to allow null = false
        if (!this.lastValue && !this.value) return false;
        return this.lastValue != this.value;
    }

    evaluateBoundValue() {
        const newValue = this.evaluator();
        this.element[this.attribute] = newValue;
        this.lastValue = newValue;
    }

    invalidate() {
        this.lastValue = this.value;
    }
}

let lastTimerUpdate = 0;
let content = undefined;
let countdownTimers = undefined;

const dataBindings = [];
const loadedViews = {};

function setLanguage(lang) {
    state.language = lang;
    applyLocalization();
}

function applyLocalization() {
    document
        .querySelectorAll(`[data-lang-${state.language}]`)
        .forEach(elm => {
            const binding = state.language == "en" ?
                elm.dataset.langEn :
                elm.dataset.langSe;

            elm.innerHTML = parseBinding(binding, state);
        });
}

function applyDataBindings() {
    document
        .querySelectorAll("*")
        .forEach(elm => {
            for (const attr in elm.dataset) {
                if (attr.startsWith("bind")) {
                    const binding = elm.dataset[attr];
                    let attributeName = attr.substring(4);
                    // make the first letter as lower case
                    attributeName = attributeName[0].toLowerCase() + attributeName.substring(1);
                    parseDataBinding(elm, attributeName, binding, state);
                }
            }
        });
}

function parseDataBinding(elm, attributeName, binding, model) {
    const elmDataBinding = dataBindings.find(x => x.element == elm && x.attribute == attributeName);
    if (elmDataBinding) {
        elmDataBinding.updateBinding(() => evaluatePropertyPath(binding, model));
    } else {
        const dataBinding = new DataBinding(elm, attributeName, binding, DataBindingMode.TwoWay);
        dataBinding.updateBinding(() => evaluatePropertyPath(binding, model));
        dataBindings.push(dataBinding);
    }
}

async function applyBindingsAsync() {
    applyLocalization();
    applyDataBindings();
}

async function setViewAsync(url) {
    try {
        state.view = url;
        if (!content) content = document.querySelector(".content");
        if (loadedViews[url]) {
            content.innerHTML = loadedViews[url];
            return;
        }
        loadedViews[url] = await getRequestAsync(`views/${url}.html`);
        content.innerHTML = loadedViews[url];
    } finally {
        // setLanguage(state.language);
        await applyBindingsAsync();
    }
}

function validateRSVP(rsvp) {
    if (!rsvp.firstName || rsvp.firstName.length == 0) return false;
    if (!rsvp.lastName|| rsvp.lastName.length == 0) return false;
    if (!rsvp.email || rsvp.email.length == 0) return false;
    return true;
}

async function sendRSVP() {
    if (!validateRSVP(state.rsvp)) {
        const msg = state.language == "en" 
            ? "Please make sure you fill in all fields before submitting."
            : "Var vänligen och fyll i alla fält innan du skickar.";

        alert(msg);        
        return;
    }
    try {
        await postRequestAsync("/api/rsvp", state.rsvp);

        state.rsvp.firstName = "";
        state.rsvp.lastName = "";
        state.rsvp.email = "";
        state.rsvp.phone = "";
        state.rsvp.attendance = {
            wedding: null,
            cermony: null
        };
        state.rsvp.food = {
            meat: null,
            fish: null,
            vegetarian: null,
            vegan: null
        }
        state.rsvp.message = "";
        render();

        const msg = state.language == "en" 
            ? "Thank you for submitting the RSVP!"
            : "Tack för att du OSAt!";

        alert(msg);
    } catch (e) {
        alert("Error submitting RSVP, please try again later.");
    }
}

async function linkClicked(elm) {
    const url = elm.dataset.url;
    await setViewAsync(url);
    closeMenu();
}

async function getRequestAsync(url) {
    const response = await fetch(url, {
        method: "GET"
    });
    return response.text();
}

async function postRequestAsync(url, model) {
    const response = await fetch(url, {
        method: "POST",
        mode: "cors", // no-cors, cors, *same-origin
        cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
        credentials: "same-origin", // include, *same-origin, omit
        headers: {
            "Content-Type": "application/json",
            // "Content-Type": "application/x-www-form-urlencoded",
        },
        body: JSON.stringify(model)
    });
    return response.text();
}

function render() {
    dataBindings.forEach(x => x.evaluateBoundValue());
}

function parseBinding(binding, model) {
    const regex = /{([^}]+)}/gm;

    return binding.replace(regex, m => {
        if (m.index == regex.lastIndex) {
            ++regex.lastIndex;
        }
        return evaluatePropertyPath(m, model);
    })
}

function evaluatePropertyPath(property, model) {
    let propertyNameOrPath = property.includes("{") ?
        property.substring(1, property.length - 1) :
        property;
    const inverse = propertyNameOrPath.startsWith("!");
    if (inverse) propertyNameOrPath = propertyNameOrPath.substring(1);
    const properties = [];
    let propertyParse = propertyNameOrPath;
    while (propertyParse.includes(".")) {
        properties.push(propertyParse.split(".")[0]);
        propertyParse = propertyParse.substring(propertyParse.indexOf(".") + 1);
    }
    properties.push(propertyParse);
    let value = model;
    for (let i = 0; i < properties.length; ++i) {
        value = value[properties[i]];
        if (isFunction(value)) {
            value = value();
        }
    }
    return (inverse && value != null) ? !value : value;
}

function setPropertyPath(property, value, model) {

    let propertyNameOrPath = property.includes("{") ?
        property.substring(1, property.length - 1) :
        property;
    const inverse = propertyNameOrPath.startsWith("!");
    if (inverse) propertyNameOrPath = propertyNameOrPath.substring(1);
    const properties = [];
    let propertyParse = propertyNameOrPath;
    while (propertyParse.includes(".")) {
        properties.push(propertyParse.split(".")[0]);
        propertyParse = propertyParse.substring(propertyParse.indexOf(".") + 1);
    }
    properties.push(propertyParse);

    for (let i = 0; i < properties.length; ++i) {
        if (i == properties.length - 1) {
            if (inverse) {
                if (model[properties[i]])
                    return;
                model[properties[i]] = !value;
            } else {
                model[properties[i]] = value;
            }
        } else {
            model = model[properties[i]];
            if (isFunction(model)) {
                model = model();
            }
        }
    }
}

function openMenu() {
    document.body.classList.add("nav-open")
}

function closeMenu() {
    document.body.classList.remove("nav-open")
}

function isFunction(functionToCheck) {
    return (
        functionToCheck && {}.toString.call(functionToCheck) === "[object Function]"
    );
}

window.addEventListener("load", () => {
    setViewAsync(state.view);
    uiTick(0);
});

function numberPad(value) {
    return `${value}`.length == 1 ? `0${value}` : value;
}

function uiTick(time) {
    if (!countdownTimers) countdownTimers = document.querySelectorAll("[data-wedding-countdown]");

    dataBindings.forEach(x => {
        if (x.detectChanges()) {
            setPropertyPath(x.binding, x.value, state);
            x.invalidate();
        }
    })

    const now = new Date().getTime();
    if (now - lastTimerUpdate >= 1000) {

        countdownTimers.forEach(x => {
            const type = x.dataset.weddingCountdown;
            const dateDiffMs = state.weddingDate.getTime() - (new Date()).getTime();
            const totalSeconds = dateDiffMs / 1000;
            const totalMinutes = totalSeconds / 60;
            const seconds = totalSeconds % 60;
            const minutes = totalMinutes % 60;
            const hours = (totalMinutes / 60) % 24;
            const days = (totalMinutes / 60 / 24);

            switch (type) {
                case "days":
                    x.innerHTML = `${parseInt(days, 10)}`;
                    break;
                case "hours":
                    x.innerHTML = `${parseInt(hours, 10)}`;
                    break;
                case "mins":
                    x.innerHTML = `${parseInt(minutes, 10)}`;
                    break;
                case "secs":
                    x.innerHTML = `${parseInt(seconds, 10)}`;
                    break;
            }
            if (x.innerHTML.length == 1) {
                x.innerHTML = "0" + x.innerHTML;
            }
        });

        lastTimerUpdate = now;
    }

    setTimeout(() => requestAnimationFrame(t => uiTick(t)), 50);
}