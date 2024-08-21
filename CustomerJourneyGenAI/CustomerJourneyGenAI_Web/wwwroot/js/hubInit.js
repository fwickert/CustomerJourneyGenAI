const connectionHubUrl = new URL('messageRelayHub', domainHub);

const connection = new signalR.HubConnectionBuilder().withUrl(connectionHubUrl.href).withAutomaticReconnect().withHubProtocol(new signalR.JsonHubProtocol()).build();


connection.start().then(function () {
    console.log("connected");
}).catch(function (err) {
    return console.error(err.toString());
});


