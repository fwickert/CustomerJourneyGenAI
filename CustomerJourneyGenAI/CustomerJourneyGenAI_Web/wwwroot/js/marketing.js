

connection.on("Personas", function (messageResponse) {
    OnlySpinOff($("#spinPersonas"));
    
    const htmlMessage = marked.parse(messageResponse);
    $("#resultPersonas").html(htmlMessage);
});

connection.on("Segments", function (messageResponse) {
    OnlySpinOff($("#spinSegments"));

    const htmlMessage = marked.parse(messageResponse);
    $("#resultSegments").html(htmlMessage);
});

connection.on("Recommendations", function (messageResponse) {
    OnlySpinOff($("#spinRecommendations"));

    const htmlMessage = marked.parse(messageResponse);
    $("#resultRecommendations").html(htmlMessage);
});



function GetPersonas() {
    var d = {        
        brand: $("#brand").val(),
        industry: $("#product").val(),
        language: $("#language").val(),
        count: 5
    }
    postAPIAsync($("#spinPersonas"), $("#resultPersonas"), $("#PersonasResult").data("url"), d);    
}

function GetSegments() {
    var d = {
        brand: $("#brand").val(),
        industry: $("#product").val(),
        language: $("#language").val(),
        count: 3,
        memory: $("#resultPersonas").text(),
    }
    postAPIAsync($("#spinSegments"), $("#resultSegments"), $("#SegmentsResult").data("url"), d);
}

//same for GetRecommendations
function GetRecommendations() {
    var d = {
        brand: $("#brand").val(),
        industry: $("#product").val(),
        language: $("#language").val(),
        memory: $("#resultSegments").text(),
    }
    postAPIAsync($("#spinRecommendations"), $("#resultRecommendations"), $("#RecommendationsResult").data("url"), d);
}

function GetView($spin, $result, $url, $data) {
    //SpinOn($spin, $result);
    OnlySpinOn($spin);
    if ($url === undefined) {
        $url = $result.data("url")
    }


    if ($url === undefined) {
        $url = $result.data("url")
    }

    $.ajax({
        type: "Post",
        data: {
            texte: $data,
            company: $("#brand").val(),
            product: $("#product").val(),
            language: $("#language").val(),
            bookType: $("#bookType").val(),
        },
        url: $url,
        cache: false
    })
        .done(function (result) {

        }).fail(function (jqXHR, textStatus, errorThrown) {

        });

}

//function OnlySpinOn($spin) {
//    $spin.removeClass("invisible");
//    $spin.css("display", "inline-block");
//}

