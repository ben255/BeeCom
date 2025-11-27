using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;



//make a empty gameobject in you scene.
//add this script too it to get messages.

//MyGameInstance.SendMessage(objectName, methodName, value);
/*
objectName is the name of an object in your scene.
methodName is the name of a method in the script, currently attached to that object.
value can be a string, a number, or can be empty.
*/

//the gameobject objectName should be _unityMessenger in your scene so it correctly gets messages.
//the method names should also not be changed but if you want something else then let me know and ill implement it benjamin_work_0@hotmail.com

namespace BeeCom{
    public class fromBeeCom : MonoBehaviour
    {   

        public static fromBeeCom Instance { get; private set; }

        public string userInfo = "";
        public byte[] imageArray;
        public string firebaseJson = "";

                // 2. Awake is called when the script instance is being loaded.
        private void Awake()
        {
            // The singleton pattern logic
            if (Instance == null)
            {
                // If there is no instance yet, make this the instance.
                Instance = this;
                // Optional: if you want this object to persist between scene loads.
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                // If an instance already exists, destroy this new one.
                Destroy(gameObject);
            }
        }

        //Sending back a json of the user playing the game, If its a guest. It will be a generic user image(can also just ignore it and add your own generic one to save compution), and 'guest' as name.
        public void setUserInfo(string message)
        {
            userInfo = message;
            Debug.Log($"User info set: {message}");
        }
        //The image as a bitmap. The images are scaled down to 200x200. 
        public void setImageByte(string base64Image)
        {
            if (!string.IsNullOrEmpty(base64Image))
            {
                // Convert the Base64 string back to a byte array.
                imageArray = Convert.FromBase64String(base64Image);
            }
        }

        //To implement firebase backend stuff.
        public void setFirebaseJson(string message)
        {
            firebaseJson = message;
        }
    }
}

