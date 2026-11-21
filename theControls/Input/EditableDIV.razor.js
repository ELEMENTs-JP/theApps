export function getContent(element) {
    if (!element) {
        return "";
    }
    return element.innerText ?? "";
}

export function setContent(element, value) {
    if (!element) {
        return;
    }
    if ((element.innerText ?? "") !== value) {
        element.innerText = value ?? "";
    }
}