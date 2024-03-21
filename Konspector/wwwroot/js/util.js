window.getSelectionStartEnd = function(element) {
    var sel = window.getSelection();
    var range = sel.getRangeAt(0);
    var preCaretRange = range.cloneRange();
    preCaretRange.selectNodeContents(element);
    preCaretRange.setEnd(range.startContainer, range.startOffset);
    var start = preCaretRange.toString().length;
    return {
        start: start,
        end: start + range.toString().length
    };
};

document.addEventListener(".HTMLContent", onInput);
function onInput(e) {
 let target = e.target;
 if (target.localName == "div") {
   if (!target.value && !target.__contenteditable) target.__contenteditable = true;
   if (target.__contenteditable) target.value = target.innerText;
 }
}