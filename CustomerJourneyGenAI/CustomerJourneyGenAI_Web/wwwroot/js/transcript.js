function GetTranscript() {
    postAPI($("#spinTranscript"), $("#TranscriptResult"), $("#TranscriptResult").data("url"), 1);
}
function GetSummarize() {
    var d = {
        conversation: $("#TranscriptResult").text(),
        language: $("#language").val(),
    }
    postAPIAsync($("#spinSummarize"), $("#resultSummarize"), $("#SummarizeResult").data("url"), d);
}

//getSentiment
function GetSentiment() {
    var d = {
        conversation: $("#resultSummarize").text(),
        language: $("#language").val(),
    }
    postAPIAsync($("#spinSentiment"), $("#resultSentiment"), $("#SentimentResult").data("url"), d);
}

//getTranslate
function GetTranslate() {
    var d = {
        conversation: $("#resultSummarize").text(),
        language: $("#languageTranslate").val(),
    }
    postAPIAsync($("#spinTranslate"), $("#resultTranslate"), $("#TranslateResult").data("url"), d);
}

//getAnonymize
function GetAnonymize() {
    var d = {
        conversation: $("#TranscriptResult").text(),
    }
    postAPIAsync($("#spinAnonymize"), $("#resultAnonymize"), $("#AnonymizeResult").data("url"), d);
}

//get next axtions
function GetNextActions() {
    var d = {
        conversation: $("#TranscriptResult").text(),
        actions: 3,
        language: $("#language").val()
    }
    postAPIAsync($("#spinNextActions"), $("#resultNextActions"), $("#NextActionsResult").data("url"), d);
}

//get composemail
function GetComposeMail() {
    var d = {
        conversation: $("#TranscriptResult").text(),
        language: $("#language").val()
    }
    postAPIAsync($("#spinComposeMail"), $("#resultComposeMail"), $("#ComposeMailResult").data("url"), d);
}


connection.on("Summarize", function (messageResponse) {
    OnlySpinOff($("#spinSummarize"));

    const htmlMessage = marked.parse(messageResponse);
    $("#resultSummarize").html(htmlMessage);
});

connection.on("Sentiment", function (messageResponse) {
    OnlySpinOff($("#spinSentiment"));

    const htmlMessage = marked.parse(messageResponse);
    $("#resultSentiment").html(htmlMessage);
});

connection.on("Translate", function (messageResponse) {
    OnlySpinOff($("#spinTranslate"));

    const htmlMessage = marked.parse(messageResponse);
    $("#resultTranslate").html(htmlMessage);
});

connection.on("Anonymize", function (messageResponse) {
    OnlySpinOff($("#spinAnonymize"));

    const htmlMessage = marked.parse(messageResponse);
    $("#resultAnonymize").html(htmlMessage);
});

connection.on("NextActions", function (messageResponse) {
    OnlySpinOff($("#spinNextActions"));

    const htmlMessage = marked.parse(messageResponse);
    $("#resultNextActions").html(htmlMessage);
});

connection.on("ComposeMail", function (messageResponse) {
    OnlySpinOff($("#spinComposeMail"));

    const htmlMessage = marked.parse(messageResponse);
    $("#resultComposeMail").html(htmlMessage);
});