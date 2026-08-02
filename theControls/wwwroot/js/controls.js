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