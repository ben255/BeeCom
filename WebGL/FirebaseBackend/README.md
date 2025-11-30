This is just an example for how you can set up your own firebase backend. 



            const firebaseConfig = {
                apiKey: "      ",
                authDomain: "       ",
                databaseURL: "          ",
                projectId: "        ",
                storageBucket: "         ",
                messagingSenderId: "               ",
                appId: "              ",
                measurementId: "             "
            };

            Youll have to fill out your own firebaseConfig with your own server and it should work. It only uses realtime database so if you need more youll have to figure it out.


            Also for security reasons youll want requests only aproved comming from b-ee.se. Check out how set that up, its somewhere in the cloud app setup, idk. Ill update this as i go, maybe with a token key for everyones game, but for now its gonna be like this.
