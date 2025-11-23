using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System;
using System.Runtime.InteropServices;


//DO NOT EDIT THESE FILES IT ONLY WORKS WITH THE SETUP IVE DONE.
//Achivements will be implemented here for now. If you have more ideas which can improve user experince let me know at benjamin_work_0@hotmail.com

namespace BeeCom{

    public static class toCom
    {
        [DllImport("__Internal")]
        public static extern void onBeeMessage(string message);

    }
}
