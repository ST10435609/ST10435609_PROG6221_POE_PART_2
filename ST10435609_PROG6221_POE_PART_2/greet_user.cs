using System;
using System.Media;

namespace ST10435609_PROG6221_POE_PART_3
{// start of namespace

    internal class greet_user
    {// start of class

        public greet_user()
        {// start of constructor

            // auto get the path of the greeting.wav file
            string full_path = AppDomain.CurrentDomain.BaseDirectory
                                        .Replace(@"\bin\Debug\", @"\greeting.wav");

            // create an instance of the sound player class
            // with an object name greeting
            SoundPlayer greeting = new SoundPlayer(full_path);

            // then greet the user
            greeting.PlaySync();

        }// end of constructor

    }// end of class

}// end of namespace