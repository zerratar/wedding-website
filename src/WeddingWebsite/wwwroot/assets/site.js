let language = "en";
let content = undefined;
let countdownTimers = undefined;

const weddingDay = new Date("2019-08-02 16:00");
const loadedViews = {};

function setLanguage(lang) {
    language = lang;
    const localizedElements = document
        .querySelectorAll(`[data-lang-${lang}]`)
        .forEach(elm => {
            elm.innerHTML = lang == "en" ? elm.dataset.langEn : elm.dataset.langSe;
        });

}

async function linkClicked(elm) {
    const url = elm.dataset.url;
    if (!content) content = document.querySelector(".content");
    if (loadedViews[url]) {
        content.innerHTML = loadedViews[url];
        return;
    }
    loadedViews[url] = await getRequestAsync(`views/${url}.html`);
    content.innerHTML = loadedViews[url];
}

async function getRequestAsync(url) {
    const response = await fetch(url, {
        method: "GET"
    });
    return response.text();
}

window.addEventListener("load", () => {
    setLanguage(language);
    draw(0);
});

function draw(time) {
    if (!countdownTimers) countdownTimers = document.querySelectorAll("[data-wedding-countdown]");

    countdownTimers.forEach(x => {
        const type = x.dataset.weddingCountdown;

        const dateDiffMs =  weddingDay.getTime() - (new Date()).getTime();

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