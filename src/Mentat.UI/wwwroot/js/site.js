// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function truncate(maxWords) {
    var para = document.getElementsByClassName("long-text");

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

truncate(35);