
window.searchComponent = {
    // Registriert den globalen Keydown-Listener für Strg + F
    registerGlobalShortcut: function (inputId) {
        // Altes Event entfernen, falls vorhanden (verhindert Mehrfach-Registrierung)
        if (window.searchComponent._shortcutHandler) {
            document.removeEventListener('keydown', window.searchComponent._shortcutHandler);
        }

        window.searchComponent._shortcutHandler = function (e) {
            // Prüfung auf Strg + F (oder Cmd + F auf Mac)
            if ((e.ctrlKey || e.metaKey) && (e.key === 'f' || e.key === 'F')) {
                const inputEl = document.getElementById(inputId);
                if (inputEl) {
                    e.preventDefault(); // Verhindert das Standard-Suchfenster des Browsers
                    inputEl.focus();
                    inputEl.select();   // Optional: Markiert bereits vorhandenen Text
                }
            }
        };

        document.addEventListener('keydown', window.searchComponent._shortcutHandler);
    },

    // Entfernt den Listener beim Zerstören der Komponente
    unregisterGlobalShortcut: function () {
        if (window.searchComponent._shortcutHandler) {
            document.removeEventListener('keydown', window.searchComponent._shortcutHandler);
            window.searchComponent._shortcutHandler = null;
        }
    }
};


window.setFocusById = function (id)
{
    var element = document.getElementById(id);

    if (element)
    {
        element.focus();
    }
};

function toggleClass(className, force)
{
    if (className)
    {
        document.body.classList.toggle(className, force);
        
    }
}

// function setBodyBackground(url) {
//     document.body.style.backgroundImage = "url('" + url + "')";
// }

function setBodyBackground(url) {
    // 1. Ausfaden
    // document.body.style.transition = "opacity 3s ease";
    // document.body.style.opacity = "0";

    const img = new Image();
    img.src = url;

    // 2. Warten bis Bild geladen ist, dann Bild tauschen & Einfaden
    img.onload = function () {
        document.body.style.backgroundImage = "url('" + url + "')";
        // document.body.style.opacity = "1";
    };
}

function InitHtmlEditor() {
    hugerte.init({
        selector: '#editor',
        plugins: 'lists link image table code emoticons fullscreen',
        toolbar: 'undo redo | bold italic | bullist numlist | link image | code',
        content_style: '.mce-content-body { background-color: #24272d !important; color: #aaa !important; outline:none !important; }'
        
    });
}



const timeOptions = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric', minute: 'numeric' };

function DateTimeShortClockFull() {
    var date = new Date();

    try {
        let dtLabel = document.getElementById("datetimeclock");

        if (dtLabel !== null) {
            dtLabel.innerText = date.toLocaleDateString("de-DE", timeOptions) + " Uhr";
            dtLabel.textContent = date.toLocaleDateString("de-DE", timeOptions) + " Uhr";
            setTimeout(DateTimeShortClockFull, 1000);
        }
    }
    catch (e) {

    }


}






window.themeManager = {

    setTheme: function (theme) {

        document.documentElement.setAttribute("data-bs-theme", theme);

        localStorage.setItem("theme", theme);
    },

    isDark: function () {

        let theme = localStorage.getItem("theme");

        if (!theme) {

            theme = window.matchMedia("(prefers-color-scheme: dark)").matches
                ? "dark"
                : "light";
        }

        document.documentElement.setAttribute("data-bs-theme", theme);

        return theme === "dark";
    }
};

window.getInnerText = element => element.innerText;

window.contentEditable = {
    getText: function (element) {
        return element.innerText;
    }
};

window.contentEditable.setText = function (element, value) {
    element.innerText = value ?? "";
};



function SetIndividualTimer(dotNetRef, methodName, ms, repeat) {
    try {
        if (repeat) {
            // Wiederholender Timer
            const intervalId = setInterval(() => {
                dotNetRef.invokeMethodAsync(methodName);
            }, ms);

            // Optional: Rückgabe der ID, falls man den Timer später stoppen möchte
            return intervalId;
        }
        else {
            // Einmaliger Timer
            const timeoutId = setTimeout(() => {
                dotNetRef.invokeMethodAsync(methodName);
            }, ms);

            return timeoutId;
        }
    }
    catch (e) {

    }
}