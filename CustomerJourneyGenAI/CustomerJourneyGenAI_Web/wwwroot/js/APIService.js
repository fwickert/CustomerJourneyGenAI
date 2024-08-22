//GetDomainBase depend on Config file
var env = document.getElementById("envData").getAttribute("data-environment");
var domainBase = "";
var domainHub = "";
switch (env) {
    case "Development":
        domainBase = "https://localhost:7165/api/";
        domainHub = "https://localhost:7165/";
        break;
    case "Production":
        domainBase = "";
        break;
    default:
        domainBase = "https://localhost:7165/api/";
}




function postAPIAsync($spin, $result, url, d) {
    OnlySpinOn($spin);
    $.ajax({
        url: domainBase + url + "?connectionId=" + connection.connectionId,
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(d),
        success: function (response) {
            console.log(response);
            if (typeof templateScript != 'undefined') {
                $result.css("display", "inherit");
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function postAPI($spin, $result, url, d) {
    OnlySpinOn($spin);
    $.ajax({
        url: domainBase + url + "?connectionId=" + connection.connectionId,
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(d),
        success: function (response) {
            OnlySpinOff($spin);

            $result.css("display", "inherit");
            $result.removeClass("invisible");
            $result.html(response);

        },
        error: function (error) {
            console.log(error);
        }
    });
}

function getAPI($spin, $result, url) {
    OnlySpinOn($spin);
    $.ajax({
        url: domainBase + url,
        type: "GET",        
        success: function (response) {
            OnlySpinOff($spin);

            $result.css("display", "inherit");
            $result.removeClass("invisible");
            $result.html(response.htmlContent);

        },
        error: function (error) {
            console.log(error);
        }
    });
}

function OnlySpinOn($spin) {
    $spin.removeClass("invisible");
    $spin.css("display", "inline-block");
}

function OnlySpinOff($spin) {
    $spin.addClass("invisible");
    $spin.css("display", "none");
}