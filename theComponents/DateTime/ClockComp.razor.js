let resizeObservers = new WeakMap();
let animationFrames = new WeakMap();

export function initClock(container, hourHand, minuteHand, secondHand) {
    if (!container) return;

    const updateSize = () => {
        const rect = container.getBoundingClientRect();
        const minDim = Math.min(rect.width, rect.height);
        const size = Math.max(50, minDim - 16);
        container.style.setProperty('--clock-size', `${size}px`);
    };

    const observer = new ResizeObserver(() => {
        updateSize();
    });
    observer.observe(container);
    resizeObservers.set(container, observer);

    requestAnimationFrame(() => {
        updateSize();
    });

    const updateTime = () => {
        const now = new Date();
        const milliseconds = now.getMilliseconds();
        const seconds = now.getSeconds() + milliseconds / 1000;
        const minutes = now.getMinutes() + seconds / 60;
        const hours = (now.getHours() % 12) + minutes / 60;

        const secondDeg = seconds * 6;
        const minuteDeg = minutes * 6;
        const hourDeg = hours * 30;

        if (secondHand) secondHand.style.transform = `translateX(-50%) rotate(${secondDeg}deg)`;
        if (minuteHand) minuteHand.style.transform = `translateX(-50%) rotate(${minuteDeg}deg)`;
        if (hourHand) hourHand.style.transform = `translateX(-50%) rotate(${hourDeg}deg)`;

        const frameId = requestAnimationFrame(updateTime);
        animationFrames.set(container, frameId);
    };

    updateTime();
}

export function stopClock(container) {
    if (!container) return;

    if (resizeObservers.has(container)) {
        resizeObservers.get(container).disconnect();
        resizeObservers.delete(container);
    }

    if (animationFrames.has(container)) {
        cancelAnimationFrame(animationFrames.get(container));
        animationFrames.delete(container);
    }
}