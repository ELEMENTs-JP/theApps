


window.downloadFileFromStream = async (fileName, contentStreamReference) => {
    try {

        const arrayBuffer = await contentStreamReference.arrayBuffer();
        const blob = new Blob([arrayBuffer]);
        const url = URL.createObjectURL(blob);
        const anchorElement = document.createElement('a');
        anchorElement.href = url;
        anchorElement.download = fileName ?? '';
        anchorElement.click();
        anchorElement.remove();
        URL.revokeObjectURL(url);

    } catch (e) {

        alert('File Download: ' + e);

    }
}




window.numberFormatter = {
    formatInput: function (element, locale) {
        let value = element.value;

        // Erlaube nur Ziffern, Komma und Punkt
        value = value.replace(/[^0-9.,-]/g, '');

        let separator = locale === 'de-DE' ? ',' : '.';
        let parts = value.split(separator);
        let integerPart = parts[0].replace(/[^0-9-]/g, '');

        if (integerPart) {
            let number = parseInt(integerPart, 10);
            if (!isNaN(number)) {
                integerPart = new Intl.NumberFormat(locale).format(number);
            }
        }

        let formatted = parts.length > 1 ? integerPart + separator + parts[1] : integerPart;

        // Wert direkt im HTML-Element setzen
        element.value = formatted;

        // Wert an C# zurückliefern
        return formatted;
    }
};


window.imageUtils = {
    compressToTargetSize: function (byteArray, maxPixelSize = 200) {
        return new Promise((resolve, reject) => {
            const blob = new Blob([byteArray]);
            const url = URL.createObjectURL(blob);
            const img = new Image();

            img.onload = () => {
                URL.revokeObjectURL(url);

                let srcWidth = img.naturalWidth || img.width;
                let srcHeight = img.naturalHeight || img.height;

                // 1. Maximale Kantenlänge auf 200px begrenzen bei gleichem Seitenverhältnis
                let targetWidth = srcWidth;
                let targetHeight = srcHeight;

                if (srcWidth > maxPixelSize || srcHeight > maxPixelSize) {
                    if (srcWidth > srcHeight) {
                        targetWidth = maxPixelSize;
                        targetHeight = Math.round((srcHeight * maxPixelSize) / srcWidth);
                    } else {
                        targetHeight = maxPixelSize;
                        targetWidth = Math.round((srcWidth * maxPixelSize) / srcHeight);
                    }
                }

                // 2. Einmalig skalieren
                const canvas = document.createElement("canvas");
                canvas.width = targetWidth;
                canvas.height = targetHeight;

                const ctx = canvas.getContext("2d");
                ctx.drawImage(img, 0, 0, targetWidth, targetHeight);

                // 3. Performante asynchrone Konvertierung zu Uint8Array (ohne Base64/atob-Overhead)
                canvas.toBlob((resultBlob) => {
                    if (!resultBlob) {
                        reject(new Error("Canvas toBlob failed"));
                        return;
                    }
                    resultBlob.arrayBuffer().then(buffer => {
                        resolve(new Uint8Array(buffer));
                    }).catch(reject);
                }, "image/jpeg", 0.80); // 80% Qualität reicht bei 200px für wenige KB
            };

            img.onerror = (err) => {
                URL.revokeObjectURL(url);
                reject(err);
            };

            img.src = url;
        });
    }
};




window.audioInterop = {
    initPlaylist: function (audio, dotNetRef, playlist) {
        if (!audio || !playlist || playlist.length === 0) return;

        let currentIndex = 0;
        audio.src = playlist[currentIndex];
        audio.load();

        const buildPayload = () => ({
            currentTime: audio.currentTime,
            duration: audio.duration,
            volume: audio.volume,
            muted: audio.muted,
            ended: audio.ended
        });

        audio.addEventListener("play", () => {
            dotNetRef.invokeMethodAsync("OnAudioPlay", buildPayload());
        });

        audio.addEventListener("pause", () => {
            dotNetRef.invokeMethodAsync("OnAudioPause", buildPayload());
        });

        audio.addEventListener("volumechange", () => {
            dotNetRef.invokeMethodAsync("OnAudioVolumeChange", buildPayload());
        });

        audio.addEventListener("ended", () => {
            dotNetRef.invokeMethodAsync("OnAudioEnded", buildPayload());
        });
    },

    playTrack: function (audio, src) {
        if (!audio || !src) return;
        if (audio.src !== src) {
            audio.src = src;
            audio.load();
        }
        audio.play();
    },

    pauseTrack: function (audio) {
        if (!audio) return;
        audio.pause();
    },

    stopTrack: function (audio) {
        if (!audio) return;
        audio.pause();
        audio.currentTime = 0;
    }
};

// window.audioInterop = {
//     initPlaylist: function (audio, dotNetRef, playlist) {
//         if (!audio || !playlist || playlist.length === 0) return;

//         let currentIndex = 0;
//         audio.src = playlist[currentIndex];
//         audio.load();

//         const buildPayload = () => ({
//             currentTime: audio.currentTime,
//             duration: audio.duration,
//             volume: audio.volume,
//             muted: audio.muted,
//             ended: audio.ended
//         });

//         audio.addEventListener("play", () => {
//             dotNetRef.invokeMethodAsync("OnAudioPlay", buildPayload());
//         });

//         audio.addEventListener("pause", () => {
//             dotNetRef.invokeMethodAsync("OnAudioPause", buildPayload());
//         });

//         audio.addEventListener("volumechange", () => {
//             dotNetRef.invokeMethodAsync("OnAudioVolumeChange", buildPayload());
//         });

//         audio.addEventListener("ended", () => {
//             dotNetRef.invokeMethodAsync("OnAudioEnded", buildPayload());
//         });
//     },

//     // Nächsten Track setzen / abspielen
//     playTrack: function (audio, src) {
//         if (!audio || !src) return;
//         if (audio.src !== src) {
//             audio.src = src;
//             audio.load();
//         }
//         audio.play();
//     },

//     // Ergänzung für die kompakte Komponente: Pause & Stop
//     pauseTrack: function (audio) {
//         if (!audio) return;
//         audio.pause();
//     },

//     stopTrack: function (audio) {
//         if (!audio) return;
//         audio.pause();
//         audio.currentTime = 0;
//     }
// };

// window.audioInterop = {
//     initPlaylist: function (audio, dotNetRef, playlist) {
//         if (!audio || !playlist || playlist.length === 0) return;

//         let currentIndex = 0;
//         audio.src = playlist[currentIndex];
//         audio.load();

//         const buildPayload = () => ({
//             currentTime: audio.currentTime,
//             duration: audio.duration,
//             volume: audio.volume,
//             muted: audio.muted,
//             ended: audio.ended
//         });

//         audio.addEventListener("play", () => {
//             dotNetRef.invokeMethodAsync("OnAudioPlay", buildPayload());
//         });

//         audio.addEventListener("pause", () => {
//             dotNetRef.invokeMethodAsync("OnAudioPause", buildPayload());
//         });

//         audio.addEventListener("volumechange", () => {
//             dotNetRef.invokeMethodAsync("OnAudioVolumeChange", buildPayload());
//         });

//         audio.addEventListener("ended", () => {
//             dotNetRef.invokeMethodAsync("OnAudioEnded", buildPayload());
//         });
//     },

//     // Nächsten Track setzen
//     playTrack: function (audio, src) {
//         if (!audio || !src) return;
//         audio.src = src;
//         audio.load();
//         audio.play();
//     }
// };




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