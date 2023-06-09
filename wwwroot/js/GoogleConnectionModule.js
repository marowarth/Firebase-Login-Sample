﻿
    import {initializeApp} from 'https://www.gstatic.com/firebasejs/9.19.1/firebase-app.js'

    // If you enabled Analytics in your project, add the Firebase SDK for Google Analytics
    import {getAnalytics} from 'https://www.gstatic.com/firebasejs/9.19.1/firebase-analytics.js'

    // Add Firebase products that you want to use
  /*  import {getAuth} from 'https://www.gstatic.com/firebasejs/9.19.1/firebase-auth.js'*/
import { getFirestore } from 'https://www.gstatic.com/firebasejs/9.19.1/firebase-firestore.js'


import { getAuth, signInWithPopup, GoogleAuthProvider } from "https://www.gstatic.com/firebasejs/9.19.1/firebase-auth.js";
    
    // TODO: Replace the following with your app's Firebase project configuration
    const firebaseConfig = {
        apiKey: "tus datos",
        authDomain: "tus datos",
        projectId: "tus datos",
        storageBucket: "tus datos",
        messagingSenderId: "tus datos",
        appId: "tus datos",
        measurementId: "tus datos"
    };

    // Initialize Firebase
const app = initializeApp(firebaseConfig);
const provider = new GoogleAuthProvider();
const auth = getAuth();
const analytics = getAnalytics(app);
function signin() {
signInWithPopup(auth, provider)
    .then((result) => {
        // This gives you a Google Access Token. You can use it to access the Google API.
        const credential = GoogleAuthProvider.credentialFromResult(result);
        const token = credential.accessToken;
        // The signed-in user info.
        const user = result.user;
        // IdP data available using getAdditionalUserInfo(result)
        // ...
        callLogin(user);
    }).catch((error) => {
        // Handle Errors here.
        const errorCode = error.code;
        const errorMessage = error.message;
        // The email of the user's account used.
        const email = error.customData.email;
        // The AuthCredential type that was used.
        const credential = GoogleAuthProvider.credentialFromError(error);
        // ...
    });
}
function callLogin(user) {
    console.log(user)
    $.ajax({
        url: "/Login/Login",
        dataType: "json",
        method: "post",
        traditional: true,
        data:
        {
            user: JSON.stringify(user)
        }
    }).done(function (response) {
        window.location.href = response.data;
    });
}
document.getElementById("mi-boton").onclick = function () {
    signin();
};



