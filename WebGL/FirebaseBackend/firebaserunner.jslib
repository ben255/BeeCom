mergeInto(LibraryManager.library, {
    FirebaseRunner_Initialize: function(gameObjectName, callbackMethodName) {
        const gameObjectNameStr = UTF8ToString(gameObjectName);
        const callbackMethodNameStr = UTF8ToString(callbackMethodName);

        // Make SendMessage available on the window object so the module can access it.
        window.unitySendMessage = SendMessage;

        const firebaseAppScript = document.createElement('script');
        firebaseAppScript.type = 'module';
        firebaseAppScript.innerHTML = `
            // Import the functions you need from the SDKs you need
            import { initializeApp } from "https://www.gstatic.com/firebasejs/12.6.0/firebase-app.js";
            import { getAnalytics } from "https://www.gstatic.com/firebasejs/12.6.0/firebase-analytics.js";
            import { getDatabase, ref, push, set, get, child } from "https://www.gstatic.com/firebasejs/12.6.0/firebase-database.js";
            // TODO: Add SDKs for Firebase products that you want to use
            // https://firebase.google.com/docs/web/setup#available-libraries
        
            // Your web app's Firebase configuration
            // For Firebase JS SDK v7.20.0 and later, measurementId is optional
            const firebaseConfig = {
                apiKey: "AIzaSyA9y7CKxU9kZTLD5hvNHq4h7TvhuorxWrA",
                authDomain: "antvslazer.firebaseapp.com",
                databaseURL: "https://antvslazer-default-rtdb.europe-west1.firebasedatabase.app",
                projectId: "antvslazer",
                storageBucket: "antvslazer.firebasestorage.app",
                messagingSenderId: "708625240514",
                appId: "1:708625240514:web:36a936eae1e8d09e8bc65b",
                measurementId: "G-859QD7VG5M"
            };
        
            // Initialize Firebase
            try {
                const app = initializeApp(firebaseConfig);
                const analytics = getAnalytics(app);
                window.firebaseDb = getDatabase(app);

                // Expose Firebase functions to the global scope
                window.firebaseRef = ref;
                window.firebasePush = push;
                window.firebaseSet = set;
                window.firebaseGet = get;
                window.firebaseChild = child;

                console.log("Firebase Initialized Successfully.");
                // Notify Unity that initialization is complete
                window.unitySendMessage("${gameObjectNameStr}", "${callbackMethodNameStr}", "Success");
            } catch (e) {
                console.error("Error initializing Firebase: ", e);
                // Notify Unity that initialization failed
                window.unitySendMessage("${gameObjectNameStr}", "${callbackMethodNameStr}", "Error: " + e.message);
            }
        `;
        document.head.appendChild(firebaseAppScript);
    },

    FirebaseRunner_SendLevel: function(level, userName) {
        var user = UTF8ToString(userName);
        if (!window.firebaseDb) {
            console.error("Firebase Database is not initialized.");
            return;
        }
        try {
            const scoresRef = window.firebaseRef(window.firebaseDb, 'scores');
            const newScoreRef = window.firebasePush(scoresRef);
            window.firebaseSet(newScoreRef, {
                level: level,
                user: user,
                timestamp: new Date().toISOString()
            });
            console.log("Level " + level + " sent to database.");
        } catch (e) {
            console.error("Error sending level to database: ", e);
        }
    },

    FirebaseRunner_GetScores: function(gameObjectName, callbackMethodName) {
        if (!window.firebaseDb) {
            console.error("Firebase Database is not initialized.");
            return;
        }

        const gameObjectNameStr = UTF8ToString(gameObjectName);
        const callbackMethodNameStr = UTF8ToString(callbackMethodName);

        const dbRef = window.firebaseRef(window.firebaseDb);
        window.firebaseGet(window.firebaseChild(dbRef, 'scores')).then((snapshot) => {
            if (snapshot.exists()) {
                // Convert the Firebase object-of-objects into an array of objects.
                const scoresObject = snapshot.val();
                const scoresArray = Object.keys(scoresObject).map(key => scoresObject[key]);
                const scoresJson = JSON.stringify(scoresArray);

                // Wrap it in a root object that JsonUtility can parse.
                const wrappedJson = `{"items":${scoresJson}}`;
                window.unitySendMessage(gameObjectNameStr, callbackMethodNameStr, wrappedJson);
            } else {
                window.unitySendMessage(gameObjectNameStr, callbackMethodNameStr, "{}");
            }
        }).catch((error) => {
            console.error("Error getting scores:", error);
        });
    },

    // You can add other functions here that you want to call from C#
    // For example, to log a custom Firebase event:
    // FirebaseRunner_LogMyEvent: function(eventName) {
    //    // code to log event to firebase analytics
    // }
});