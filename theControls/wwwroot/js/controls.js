


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