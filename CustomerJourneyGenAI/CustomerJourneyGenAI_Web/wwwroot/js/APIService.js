var base = document.getElementById("envData").getAttribute("data-domainBase");
var domainBase = base + "api/";
var domainHub = base;


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