function truncate(maxWords, classElement) {
    var para = document.getElementsByClassName(classElement);

    for (var j = 0; j < para.length; j++) {
        var text = para[j].innerHTML;
        para[j].innerHTML = "";
        var words = text.split(" ");

        for (var i = 0; i < maxWords && i < words.length; i++) {
            para[j].innerHTML += words[i] + " ";
        }

        if (words.length >= maxWords) {
            para[j].innerHTML += "...";
        }
    }
}

// truncate questions to 35-words
truncate(35, "long-text");
// truncate titles to 15-words
truncate(15, "long-title")