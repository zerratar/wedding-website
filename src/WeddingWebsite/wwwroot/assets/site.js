const state = {
    language: "en",
    weddingDate: new Date("2019-08-02 16:00"),
    view: "wedding" // welcome
};

let content = undefined;
let countdownTimers = undefined;

const loadedViews = {};

function setLanguage(lang) {
    state.language = lang;
    document
        .querySelectorAll(`[data-lang-${lang}]`)
        .forEach(elm => {
            elm.innerHTML = lang == "en" ?
                elm.dataset.langEn :
                elm.dataset.langSe;
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
    setViewAsync(url);
}

async function getRequestAsync(url) {
    const response = await fetch(url, {
        method: "GET"
    });
    return response.text();
}

window.addEventListener("load", () => {
    setViewAsync(state.view);
    draw(0);
});

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