const connectionHubUrl = new URL('messageRelayHub', domainHub);

const connection = new signalR.HubConnectionBuilder().withUrl(connectionHubUrl.href).withAutomaticReconnect().withHubProtocol(new signalR.JsonHubProtocol()).build();

connection.start().then(function () {
    console.log("connected");
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("Personas", function (messageResponse) {
    OnlySpinOff($("#spinPersonas"));
    
    const htmlMessage = marked.parse(messageResponse);
    $("#resultPersonas").html(htmlMessage);
});


function GetPersonas() {
    var d = {        
        brand: $("#brand").val(),
        industry: $("#product").val(),
        language: $("#language").val(),
        count: 3
    }
    postAPI($("#spinPersonas"), $("#resultPersonas"), $("#PersonasResult").data("url"), d);
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

