using System;
using System.Collections.Generic;
using System.Text;

namespace ST10435609_PROG6221_POE_PART_2
{//start of namespace

    public class activity_log
    {//start of class

        // list to store all the actions with timestamps
        private List<string> log = new List<string>();


        // method to add an action to the log with a timestamp
        public void add_entry(string action)
        {//start of add_entry method

            string entry = "[" + DateTime.Now.ToString("HH:mm:ss") + "] " + action;
            log.Add(entry);

        }//end of add_entry method


        // method to return the last 10 actions as a formatted string
        public string get_log()
        {//start of get_log method

            if (log.Count == 0)
            {//start of if statement
                return "No actions have been recorded yet.";
            }//end of if statement

            // only show the last 10 actions to keep it concise
            int startIndex = Math.Max(0, log.Count - 10);
            List<string> recent = log.GetRange(startIndex, log.Count - startIndex);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Here's a summary of recent actions:");

            int count = 1;
            foreach (string entry in recent)
            {//start of foreach loop
                sb.AppendLine(count + ". " + entry);
                count++;
            }//end of foreach loop

            return sb.ToString();

        }//end of get_log method


    }//end of class

}//end of namespace