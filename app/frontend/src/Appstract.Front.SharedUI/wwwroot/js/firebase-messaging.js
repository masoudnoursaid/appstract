const app_key = "BC6wRB5G4ue_K6wZlqmO3YTvU5EDLoYWGC8uLfjP7gcbqf4oYZCdDQ3tqut4HB3WcZUtlwbOC-u75DyNgjG4MDk";
const firebaseConfig = {
    apiKey: "AIzaSyC1pQfRoViBsmvTpqqHVlAqab3MVXTFyhs",
    authDomain: "ultratone-4d7f6.firebaseapp.com",
    databaseURL: "https://ultratone-4d7f6.firebaseio.com",
    projectId: "ultratone-4d7f6",
    storageBucket: "ultratone-4d7f6.appspot.com",
    messagingSenderId: "111615641978",
    appId: "1:111615641978:web:6c50969dea7c49048d6ec3",
    measurementId: "G-NQWMDBZZRR"
};

// Initialize Firebase
firebase.initializeApp(firebaseConfig);
const m = firebase.messaging();

// Handle incoming messages. Called when:
// - a message is received while the app has focus
// - the user clicks on an app notification created by a service worker
m.onMessage(function (payload) {
    const theNotification = payload.notification

    if (Notification.permission === "granted")
        onNotification(theNotification);
});

function sendTokenToDB(done) {
    m.getToken({
        vapidKey: app_key
    }).then((currentToken) => {
        if (currentToken) {
            localStorage.setItem('FirebaseToken', currentToken);
        }
    }).catch((err) => {
        console.log('An error occurred while retrieving token. ', err);
    });
}

function onNotification(theNotification) {
    console.log(theNotification);
    const {title, link_url, ...options} = theNotification;
    const notification_options = {
        tag: "notification 1",
        renotify: true,
        click_action: "/", // To handle notification click when notification is moved to notification tray
        data: {
            click_action: "/"
        }
    }
    notification_options.data.link_url = link_url;

    if ('serviceWorker' in navigator) {
        // this will register the service worker or update it. More on service worker soon
        navigator.serviceWorker.register('./firebase-messaging-sw.js', {scope: './'}).then(function (registration) {
            console.log("Service Worker Registered");
            setTimeout(() => {
                // display the notificaiton
                registration.showNotification(title, {...notification_options, ...options}).then(done => {
                    console.log("sent notificaiton to user");
                    // const audio = new Audio("./util/sound/one_soft_knock.mp3"); // only works on windows chrome
                    // audio.play();
                }).catch(err => {
                    console.error("Error sending notificaiton to user", err);
                });
                registration.update();
            }, 100);
        }).catch(function (err) {
            console.log("Service Worker Failed to Register", err);
        });
    }
}

function registerUserFCM() {
    if (!("Notification" in window)) {
        // Check if the browser supports notifications
    } else if (Notification.permission === "granted") {
        // Check whether notification permissions have already been granted;
        // if so, create a token for that user and send to server
        sendTokenToDB(done => {
            console.log("done", done);
            if (done) {
                onNotification({title: "Successful", body: "Your device has been register", tag: "welcome"});
            }
        });
    } else if (Notification.permission !== "denied") {
        // We need to ask the user for permission
        Notification.requestPermission().then((permission) => {
            // If the user accepts, create a token and send to server
            if (permission === "granted") {
                sendTokenToDB(done => {
                    console.log("done", done);
                    if (done) {
                        onNotification({title: "Successful", body: "Your device has been register", tag: "welcome"});
                    }
                });
            } else {
                alert("You won't be able to receive important notifications 😥!");
            }
        });
    }
}
