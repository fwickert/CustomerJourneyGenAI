
function GetPrompt(functionName, plugin) {
    var d = {
        functionName: functionName,
        plugin: plugin,
    };
    var url = $("#PromptResult").data("url");

    postAPI($("#spinPrompt"), $("#resultPrompt"), url, d)
    
}