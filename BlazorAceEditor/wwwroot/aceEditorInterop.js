import "./lib/ace/ace.js";

let editor;
export function init(elem, options) {
    var element = document.getElementById(elem);
    if (element.height < 1) {
        element.setStyle("height", "30rem");
    }
    ace.config.set("basePath", "_content/BlazorAceEditor/lib/ace");
    editor = ace.edit(elem, options);
}
export function getValue() {
    return editor.getValue();
}
export function setValue(value) {
    editor.setValue(value);
}
