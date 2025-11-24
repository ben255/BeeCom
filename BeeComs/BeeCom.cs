using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.InteropServices;


//DO NOT EDIT THESE FILES IT ONLY WORKS WITH THE SETUP IVE DONE.
//Achivements will be implemented here for now. If you have more ideas which can improve user experince let me know at benjamin_work_0@hotmail.com

namespace BeeCom{

    public static class toBeeCom
    {

        public enum MessageType
        {
            QUITGAME
        }
        [DllImport("__Internal")]
        public static extern void onBeeMessage(string message);

        

        //I think its only reasnable to use this function to send messages as this is all thats implemented on the website. Sending your own wont do anything really.
        public static void onBeeMessages(MessageType message)
        {
            switch (message)
            {
                case MessageType.QUITGAME:
                    onBeeMessage("quitgame");
                    break;
                default:
                    break;
            }
        }
    }
}
