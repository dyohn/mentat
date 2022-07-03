function toggleShowHideOfFlashCard(index) {
    var showHideLink = $("[id$=show-hide_" + index + "]");
    if (showHideLink.text() === "Hide") {
        hideCardAndClearOverlay(showHideLink, index);
    } else {
        showHideLink.text("Hide");
        $("[id$=CardAnswerOverlay_" + index + "]").val($("[id$=HiddenCardAnswer_" + index + "]").val());
    }  
}

function hideCardAndClearOverlay(showHideLink, index) {
    showHideLink.text("Show");
    $("[id$=CardAnswerOverlay_" + index + "]").val("");
}

function goToPrevious(index) {
    var cardCount = getCardCountAndHideCurrentCard(index);
    var divToShow = index === 0
        ? cardCount - 1
        : index - 1;
    $("[id$=flashcard_" + divToShow + "]").show();
    hideAnswerOfCardNoLongerVisible(index);
}

function goToNext(index) {
    var cardCount = getCardCountAndHideCurrentCard(index);
    var divToShow = index === cardCount - 1
        ? 0
        : index + 1;
    $("[id$=flashcard_" + divToShow + "]").show();
    hideAnswerOfCardNoLongerVisible(index);
}

function getCardCountAndHideCurrentCard(index) {
    $("[id$=flashcard_" + index + "]").hide();
    return $("#NumberOfFlashCards").val();
}

function hideAnswerOfCardNoLongerVisible(index) {
    var showHideLink = $("[id$=show-hide_" + index + "]");
    if (showHideLink.text() === "Hide") {
        hideCardAndClearOverlay(showHideLink, index);
    }
}