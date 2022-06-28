function toggleShowHideOfFlashCard() {
    var showHideLink = $("#show-hide");
    if (showHideLink.text() === "Hide") {
        showHideLink.text("Show");
        $("#CardAnswer").val("");
    } else {
        showHideLink.text("Hide");
        $("#CardAnswer").val($("#HiddenCardAnswer").val());
    }  
}