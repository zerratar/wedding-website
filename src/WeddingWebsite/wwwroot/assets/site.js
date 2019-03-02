const state = {
    language: "en",
    weddingDate: new Date("2019-08-02 16:00"),
    view: "welcome", // welcome
    address: "A O Elliots Väg 10, 413 11 Göteborg",
    contact: { 
        bride: { name: "Nathalie", phone: "555-000-124", email: "nathalie@worldwideweb.com" },
        groom: { name: "Karl", phone: "555-000-123", email: "karl@worldwideweb.com" }
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

let content = undefined;
let countdownTimers = undefined;

const loadedViews = {};

function setLanguage(lang) {
    state.language = lang;
    document
        .querySelectorAll(`[data-lang-${lang}]`)
        .forEach(elm => {

            const binding = lang == "en" ?
                elm.dataset.langEn :
                elm.dataset.langSe;

            elm.innerHTML = parseBinding(binding, state);
        });
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
        setLanguage(state.language);
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

function parseBinding(binding, model) {
    const regex = /{([^}]+)}/gm;

    return binding.replace(regex, m => {
        if (m.index == regex.lastIndex) {
            ++regex.lastIndex;
        }

        const propertyNameOrPath = m.substring(1, m.length - 1);
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
        return value;
    })
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
    draw(0);
});

function numberPad(value) {
    return `${value}`.length == 1 ? `0${value}` : value;
}

function draw(time) {
    if (!countdownTimers) countdownTimers = document.querySelectorAll("[data-wedding-countdown]");

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

    setTimeout(() => requestAnimationFrame(t => draw(t)), 1000);
}