importScripts("https://cdnjs.cloudflare.com/ajax/libs/firebase/10.0.0/firebase-app-compat.min.js");
importScripts("https://cdnjs.cloudflare.com/ajax/libs/firebase/10.0.0/firebase-messaging-compat.min.js");

// Initialize the Firebase app in the service worker by passing in the
// messagingSenderId.
firebase.initializeApp({
    apiKey: "AIzaSyC1pQfRoViBsmvTpqqHVlAqab3MVXTFyhs",
    authDomain: "ultratone-4d7f6.firebaseapp.com",
    databaseURL: "https://ultratone-4d7f6.firebaseio.com",
    projectId: "ultratone-4d7f6",
    storageBucket: "ultratone-4d7f6.appspot.com",
    messagingSenderId: "111615641978",
    appId: "1:111615641978:web:6c50969dea7c49048d6ec3",
    measurementId: "G-NQWMDBZZRR"
});

// Retrieve an instance of Firebase Messaging so that it can handle background
// messages.
const messaging = firebase.messaging();

messaging.onBackgroundMessage((payload) => {
    console.log("Received background message", payload);
    const {title, link_url, ...options} = payload.notification; // = payload.data
    // const notification_options = {
    //     icon: "/img/icon.png",
    //     tag: "notification 1",
    //     badge: "/img/icon.png",
    //     image: "/img/background/sm.jpg",
    //     renotify: true,
    //     click_action: "/", // To handle notification click when notification is moved to notification tray
    //     data: {
    //         click_action: "/"
    //     }
    // }
    const notification_options = {
        tag: "notification 1", 
        renotify: true, 
        click_action: "/", // To handle notification click when notification is moved to notification tray
        data: {
            click_action: "/"
        }
    }
    notification_options.data.link_url = link_url;

    // Customize notification here
    // you can show the notification here again, but it would called twice
    // self.registration.showNotification(title, {...notification_options, ...options});
});

self.addEventListener("notificationclick", (event) => {
    event.notification.close();

    event.waitUntil(clients.matchAll({type: "window"}).then((clientList) => {
        console.log("what is client list", clientList);
        for (const client of clientList) {
            if (client.url === "/" && "focus" in client) return client.focus();
        }
        if (clients.openWindow && Boolean(event.notification.data.link_url)) return clients.openWindow(event.notification.data.link_url);
    }).catch(err => {
        console.log("There was an error waitUntil:", err);
    }));
});
