window.onload = function () {
    $('#deleteModal').on('shown.bs.modal', function (event) {
        var cardID = $(event.relatedTarget).data("id");
        $("#confirmDelete").attr("data-id-to-delete", cardID);
    });

    $('#SelectedDifficulties').multiselect();
};

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
    var cardCount = getCardCount();
    var indexToShow = index === 1
        ? cardCount
        : index - 1;
    processCardChange(index, indexToShow);
    updateSelectedCardIndex(indexToShow);
}

function goToNext(index) {
    var cardCount = getCardCount();
    var indexToShow = index === cardCount
        ? 1
        : index + 1;
    processCardChange(index, indexToShow);
    updateSelectedCardIndex(indexToShow);
}

function processCardChange(index, indexToShow) {
    hideCurrentCard(index);
    showCard(indexToShow);
    hideAnswerOfCardNoLongerVisible(index);
    $("#CurrentIndex").val(indexToShow);
}

function getCardCount() {
    return parseInt($("#NumberOfFlashCards").val());
}

function hideCurrentCard(index) {
    $("[id$=flashcard_" + index + "]").hide();
}

function showCard(indexToShow) {
    $("[id$=flashcard_" + indexToShow + "]").show();
}

function hideAnswerOfCardNoLongerVisible(index) {
    var showHideLink = $("[id$=show-hide_" + index + "]");
    if (showHideLink.text() === "Hide") {
        hideCardAndClearOverlay(showHideLink, index);
    }
}

function updateSelectedCardIndex(newIndex) {
    $("#SelectedCardIndex").val(newIndex);
}

function updateSelectedCard(sender) {
    var newIndex = parseInt(sender.value);
    var cardCount = getCardCount();
    var isNewCardIndexValid = /^\d+$/.test(sender.value) && newIndex > 0 && newIndex <= cardCount;
    if (!isNewCardIndexValid) {
        toastr.error("Please choose a valid Flashcard (1&nbsp;through&nbsp;" + cardCount + ").", "Flashcard selection");
        return;
    }
    processCardChange(parseInt($("#CurrentIndex").val()), newIndex);
}

function refreshFlashcardList() {
    window.location.reload();
}

function deleteFlashcard(sender) {
    $.post("/Card/DeleteCard",
        {
            cardID: $(sender).data("id-to-delete")
        },
        function (result) {
            if (result === "ok") {
                $('#confirmDelete').modal('hide');
                refreshFlashcardList();
            }
        }
    ); 
}

function onClickFilterAccordian() {
    $("#filters").collapse("toggle");
    $("#caret-right").toggleClass(["fa-caret-down", "fa-caret-right"]);
}

function tagFieldKeyup(e) {
    // If the user enters a comma, then embed a new element in the input field
    // containing the existing text that represents the tag.
    if (event.keyCode === 188) {
        e.parentNode.insertBefore(createTagItem(e.value), e);
        e.value = "";
    }
    function createTagItem(text) {
        const item = document.createElement("div");
        item.setAttribute("class", "tag-item");
        // strip off the text after and including the last comma (tags can't contain commas, so there should only be one anyway)
        text = text.substr(0, text.lastIndexOf(','));
        const span = `<span class="tag-value">${text}</span>`;
        const close = `<div class="fa fa-close" onclick="this.parentNode.remove()"></div>`;
        item.innerHTML = span + close;
        return item;
    }
}

function prepareTagList() {
    let stringArray = [];
    document.querySelectorAll('span.tag-value').forEach(function (element) {
        stringArray.push(element.textContent.trim());
    });
    let stringList = stringArray.join(',');
    document.getElementById('tagList').value = stringList;
}