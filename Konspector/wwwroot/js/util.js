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