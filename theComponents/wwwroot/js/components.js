let clockIntervalId = null;

window.startClock = (dotNetHelper) => {
    function sendTimeToDotNet() {
        // Formatiert die Uhrzeit nach lokalen Client-Einstellungen im Kurzformat mit Sekunden (z. B. 14:05:09)
        const now = new Date();
        const timeString = now.toLocaleTimeString([], { 
            hour: '2-digit', 
            minute: '2-digit', 
            second: '2-digit' 
        });
        
        dotNetHelper.invokeMethodAsync('UpdateTimeFromClient', timeString);
    }

    // Sofort ausführen und danach jede Sekunde
    sendTimeToDotNet();
    clockIntervalId = setInterval(sendTimeToDotNet, 1000);
};

window.stopClock = () => {
    if (clockIntervalId) {
        clearInterval(clockIntervalId);
        clockIntervalId = null;
    }
};