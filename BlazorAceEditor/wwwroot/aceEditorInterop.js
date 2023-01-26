import "./lib/ace/ace.js";

let editor;
export function init(elem, options) {
    const element = document.getElementById(elem);
    if (element.height < 1) {
        element.setStyle("height", "30rem");
    }
    ace.config.set("basePath", "_content/BlazorAceEditor/lib/ace");
    editor = ace.edit(elem, options);
    if (!editor)
        return false;
    return true;
}
export function getValue() {
    return editor.getValue();
}
export function setValue(value) {
    editor.setValue(value);
}
export function setLanguage(lang) {
    editor.session.setMode(`ace/mode/${lang}`);
}
export function setTheme(theme) {
    editor.setTheme(`ace/theme/${theme}`);
}
export function availableThemes() {
    return getThemes();
}
var themeData = [
    ["Chrome"],
    ["Clouds"],
    ["Crimson Editor"],
    ["Dawn"],
    ["Dreamweaver"],
    ["Eclipse"],
    ["GitHub"],
    ["IPlastic"],
    ["Solarized Light"],
    ["TextMate"],
    ["Tomorrow"],
    ["XCode"],
    ["Kuroir"],
    ["KatzenMilch"],
    ["SQL Server", "sqlserver", "light"],
    ["Ambiance", "ambiance", "dark"],
    ["Chaos", "chaos", "dark"],
    ["Clouds Midnight", "clouds_midnight", "dark"],
    ["Dracula", "", "dark"],
    ["Cobalt", "cobalt", "dark"],
    ["Gruvbox", "gruvbox", "dark"],
    ["Green on Black", "gob", "dark"],
    ["idle Fingers", "idle_fingers", "dark"],
    ["krTheme", "kr_theme", "dark"],
    ["Merbivore", "merbivore", "dark"],
    ["Merbivore Soft", "merbivore_soft", "dark"],
    ["Mono Industrial", "mono_industrial", "dark"],
    ["Monokai", "monokai", "dark"],
    ["Nord Dark", "nord_dark", "dark"],
    ["One Dark", "one_dark", "dark"],
    ["Pastel on dark", "pastel_on_dark", "dark"],
    ["Solarized Dark", "solarized_dark", "dark"],
    ["Terminal", "terminal", "dark"],
    ["Tomorrow Night", "tomorrow_night", "dark"],
    ["Tomorrow Night Blue", "tomorrow_night_blue", "dark"],
    ["Tomorrow Night Bright", "tomorrow_night_bright", "dark"],
    ["Tomorrow Night 80s", "tomorrow_night_eighties", "dark"],
    ["Twilight", "twilight", "dark"],
    ["Vibrant Ink", "vibrant_ink", "dark"]
];

function getThemes() {
    const themes = themeData.map(function (data) {
        const name = data[1] || data[0].replace(/ /g, "_").toLowerCase();
        const theme = {
            Caption: data[0],
            Theme: `ace/theme/${name}`,
            IsDark: data[2] === "dark",
            Name: name
        };
        
        return theme;
    });
    return themes;
}
