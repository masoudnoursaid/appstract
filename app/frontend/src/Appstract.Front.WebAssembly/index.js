import * as JsSIP from "jssip"

window.RegisterWithServer = function (instance, server, port, username, password, display_name) {

    // Create our JsSIP instance and run it
    var socket = new JsSIP.WebSocketInterface(`wss://${server}:${port}/ws`);
    var configuration = {
        sockets: [socket],
        registrar_server: server,
        uri: `sip:${username}@${server}`,
        display_name,
        password
    };

    var phone = new JsSIP.UA(configuration);

    // WebSocket connection events
    phone.on('connected', function (e) { instance.invokeMethodAsync('OnConnected'); });
    phone.on('disconnected', function (e) { instance.invokeMethodAsync('OnDisconnected'); });

    // SIP registration events
    phone.on('registered', function (e) { instance.invokeMethodAsync('OnRegistered'); });
    phone.on('unregistered', function (e) { instance.invokeMethodAsync('OnUnregistered'); });
    phone.on('registrationFailed', function (e) { instance.invokeMethodAsync('OnRegistrationFailed'); });

    phone.start();
}
