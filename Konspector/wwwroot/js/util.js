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

function saveStr(str) {
   // Create element with <a> tag
   const link = document.createElement("a");

   // Create a blog object with the file content which you want to add to the file
   const file = new Blob([str], { type: 'text/plain' });

   // Add file content in the object URL
   link.href = URL.createObjectURL(file);

   // Add file name
   link.download = "sample.txt";

   // Add click event to <a> tag to save file.
   link.click();
   URL.revokeObjectURL(link.href);
}