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

//the gameobject objectName should be _unityMessanger in your scene so it correctly gets messages.
//the method names should also not be changed but if you want something else then let me know and ill implement it benjamin_work_0@hotmail.com

namespace BeeCom{
    public class fromBeeCom : MonoBehaviour
    {   

        public static string userInfo = "";
        public static string userBitmap = "";
        public static string firebaseJson = "";

        //Sending back a json of the user playing the game, If its a guest. It will be a generic user image(can also just ignore it and add your own generic one to save compution), and 'guest' as name.
        public static void setUserInfo(string message)
        {
            userInfo = message;
        }
        //The image as a bitmap
        public static void setUserBitmap(string message)
        {

        }

        //To implement firebase backend stuff.
        public static void setFirebaseJson(string message)
        {

        }
    }
}