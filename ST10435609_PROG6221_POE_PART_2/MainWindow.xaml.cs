using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ST10435609_PROG6221_POE_PART_3
{// start of namespace

    public partial class MainWindow : Window
    {   //start of class

        // -------------------------------------------------------
        // PART 2 FIELDS
        // -------------------------------------------------------

        // store the username so we can use it in responses
        private string userName = "";

        // memory - remembers the last topic the user asked about
        // used for follow up questions like "tell me more"
        private string lastTopic = "";

        // memory - stores the user's favourite cybersecurity topic
        private string favouriteTopic = "";

        // used to randomly pick a response from a list
        private Random random = new Random();

        // dictionary that maps keywords to a list of possible responses
        private Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            {
                "password", new List<string>
                {
                    "Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords.",
                    "A strong password is at least 12 characters long and mixes uppercase, lowercase, numbers, and symbols.",
                    "Consider using a password manager so you never have to reuse passwords across different sites.",
                    "Never share your password with anyone — not even IT support. Legitimate staff will never ask for it."
                }
            },
            {
                "scam", new List<string>
                {
                    "Scammers often create urgency — 'Act now or lose your account!' Slow down and verify before acting.",
                    "If an offer sounds too good to be true, it probably is. Research the source before clicking any links.",
                    "Report scam messages to your email provider and relevant authorities to help protect others.",
                    "Common scams include fake prize notifications, charity fraud, and romance scams. Stay alert!"
                }
            },
            {
                "privacy", new List<string>
                {
                    "Review your social media privacy settings regularly to control who sees your information.",
                    "Use a VPN on public Wi-Fi networks to keep your browsing private and encrypted.",
                    "Be careful about what personal information you share online — it can be used against you.",
                    "Enable two-factor authentication wherever possible to add an extra layer of privacy protection."
                }
            },
            {
                "phishing", new List<string>
                {
                    "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
                    "Always check the sender's actual email address — not just the display name — before clicking links.",
                    "Hover over links before clicking to see the real URL. If it looks suspicious, don't click.",
                    "Legitimate banks and companies will never ask for your password or PIN via email or SMS."
                }
            },
            {
                "malware", new List<string>
                {
                    "Keep your antivirus software up to date to defend against the latest malware threats.",
                    "Never download software from untrusted websites — malware is often hidden in free downloads.",
                    "Ransomware can lock your files. Regular backups are your best defence against losing your data.",
                    "A sudden slowdown in your computer or unexpected pop-ups can be signs of malware infection."
                }
            },
            {
                "safe browsing", new List<string>
                {
                    "Always look for 'https://' and a padlock icon before entering personal information on a website.",
                    "Keep your browser and its extensions up to date — updates often patch critical security holes.",
                    "Avoid clicking on pop-up ads. They can redirect you to malicious websites.",
                    "Use a reputable ad blocker to reduce exposure to malicious advertisements while browsing."
                }
            },
            {
                "2fa", new List<string>
                {
                    "Two-factor authentication (2FA) adds a second verification step, making it much harder for attackers to access your accounts.",
                    "Enable 2FA on all accounts that support it, especially email, banking, and social media.",
                    "Use an authenticator app instead of SMS for 2FA — it's more secure against SIM-swap attacks."
                }
            },
            {
                "vpn", new List<string>
                {
                    "A VPN encrypts your internet traffic and hides your IP address, protecting you on public Wi-Fi.",
                    "Choose a reputable VPN provider with a no-logs policy so your browsing stays private.",
                    "While a VPN improves privacy, it doesn't make you completely anonymous — stay vigilant."
                }
            },
            {
                "firewall", new List<string>
                {
                    "A firewall monitors incoming and outgoing network traffic to block suspicious connections.",
                    "Make sure your operating system's built-in firewall is always enabled for basic protection.",
                    "Enterprise firewalls can detect and block threats before they even reach your device."
                }
            }
        };

        // list of phrases that mean the user wants more info on the same topic
        private List<string> moreTriggers = new List<string>
        {
            "tell me more", "explain more", "give me another tip", "more info",
            "another tip", "keep going", "continue", "go on", "elaborate"
        };

        // -------------------------------------------------------
        // PART 3 - OBJECTS FROM THEIR OWN CLASS FILES
        // -------------------------------------------------------

        // create an instance of the tasks class - from tasks.cs
        tasks manage_task = new tasks();

        // create an instance of the quiz class - from quiz.cs
        quiz cyber_quiz = new quiz();

        // create an instance of the activity_log class - from activity_log.cs
        activity_log log = new activity_log();

        // -------------------------------------------------------
        // PART 3 - TASK 1 FIELDS
        // flag and temp variables for the reminder flow
        // -------------------------------------------------------

        // flag to track if we are waiting for the user's reminder reply
        private bool waitingForReminder = false;

        // temp variables to hold the task while waiting for reminder
        private string pendingTaskName = "";
        private string pendingTaskDescription = "";


        // -------------------------------------------------------
        // CONSTRUCTOR
        // -------------------------------------------------------
        public MainWindow()
        {//start of constructor
            InitializeComponent();

        }//end of constructor


        // -------------------------------------------------------
        // LOGO SCREEN
        // -------------------------------------------------------
        private void start_ai(object sender, RoutedEventArgs e)
        {// start of method

            // play greeting sound - carried over from Part 1 and Part 2
            new greet_user();

            // set the logo page grid to be invisible
            logo_grid.Visibility = Visibility.Hidden;

            // set the username page grid to be visible
            username_grid.Visibility = Visibility.Visible;

        }// end of start_ai method


        // -------------------------------------------------------
        // USERNAME SCREEN
        // -------------------------------------------------------
        private void submit_name(object sender, RoutedEventArgs e)
        {// start of submit_name method

            // collect the username from the text box
            string collect_username = user_name.Text.ToString();

            // check if the name is empty or not
            if (collect_username != "")
            {// start of if statement

                // save the username so we can use it later in responses
                userName = collect_username;

                // show message
                MessageBox.Show("Welcome " + collect_username + " to the awareness AI chatbot!");

                // set the username page grid to be invisible
                username_grid.Visibility = Visibility.Hidden;

                // set the chats page grid to be visible
                chats_grid.Visibility = Visibility.Visible;

                // add the welcome message to the chat list
                chats_list.Items.Add("Bot: Welcome " + collect_username + " to the Cybersecurity Awareness Bot!");
                chats_list.Items.Add("Bot: How can I help you today? You can ask me about:");
                chats_list.Items.Add("Bot: Passwords | Phishing | Safe Browsing | Scams | Privacy | Malware | 2FA | VPN | Firewall");
                chats_list.Items.Add("Bot: You can also type 'add task', 'view tasks', 'start quiz', or 'show activity log'.");

                // log the session start - activity_log.cs
                log.add_entry("Session started by " + userName + ".");

            }// end of if statement

            else
            {// start of else statement

                // show message
                MessageBox.Show("Please enter your username to continue!");

            }//end of else statement

        }// end of submit_name method


        // -------------------------------------------------------
        // SEND MESSAGE - main chat handler
        // -------------------------------------------------------
        private void send_question(object sender, RoutedEventArgs e)
        {// start of send_question method

            // get the text the user typed in the input box
            string input = user_input.Text.ToString().Trim();

            // check if the input is empty
            if (input == "")
            {// start of if statement
                chats_list.Items.Add("Bot: Please type something so I can help you.");
                return;
            }// end of if statement

            // add the user message to the chat list
            chats_list.Items.Add(userName + ": " + input);

            // clear the input box after sending
            user_input.Clear();

            // if the quiz is active, send input to the quiz handler - quiz.cs
            if (cyber_quiz.quizActive)
            {// start of if statement
                handle_quiz_answer(input);
                return;
            }// end of if statement

            // if we are waiting for a reminder reply, handle that - tasks.cs
            if (waitingForReminder)
            {// start of if statement
                handle_reminder_reply(input);
                return;
            }// end of if statement

            // check the mood of the user and respond accordingly
            DetectAndShowSentiment(input);

            // generate a response and add it to the chat list
            string response = GenerateResponse(input.ToLower());
            chats_list.Items.Add("Bot: " + response);

            // scroll to the latest message so the user can see it
            chats_list.ScrollIntoView(chats_list.Items[chats_list.Items.Count - 1]);

        }// end of send_question method


        // -------------------------------------------------------
        // MAIN RESPONSE ENGINE
        // -------------------------------------------------------
        private string GenerateResponse(string input)
        {// start of GenerateResponse method

            // -----------------------------------------------
            // TASK 4 - show the activity log - activity_log.cs
            // -----------------------------------------------
            if (input.Contains("show activity log") || input.Contains("what have you done for me") || input.Contains("activity log"))
            {// start of if statement
                return log.get_log();
            }// end of if statement

            // -----------------------------------------------
            // TASK 2 - start the quiz - quiz.cs
            // -----------------------------------------------
            if (input.Contains("start quiz") || input.Contains("quiz me") || input.Contains("take quiz") || input.Contains("play quiz"))
            {// start of if statement
                return start_quiz();
            }// end of if statement

            // -----------------------------------------------
            // TASK 1 - task manager commands
            // TASK 3 - NLP: recognise different phrasings
            // -----------------------------------------------
            if (input.Contains("add task") || input.Contains("create task") || input.Contains("new task") ||
                (input.Contains("add") && input.Contains("task")))
            {// start of if statement
                return handle_add_task(input);
            }// end of if statement

            if (input.Contains("view task") || input.Contains("show task") || input.Contains("list task") ||
                input.Contains("my tasks") || input.Contains("see my tasks"))
            {// start of if statement
                return open_tasks_page();
            }// end of if statement

            if (input.Contains("complete task") || input.Contains("mark task") || input.Contains("done with task"))
            {// start of if statement
                return handle_complete_task(input);
            }// end of if statement

            if (input.Contains("delete task") || input.Contains("remove task"))
            {// start of if statement
                return handle_delete_task(input);
            }// end of if statement

            // -----------------------------------------------
            // CONVERSATION FLOW - follow up triggers
            // -----------------------------------------------
            if (moreTriggers.Any(t => input.Contains(t)))
            {// start of if statement
                if (lastTopic != "" && keywordResponses.ContainsKey(lastTopic))
                {
                    log.add_entry("Follow-up tip given on '" + lastTopic + "'.");
                    return GetRandomResponse(lastTopic);
                }
                return "Sure! Could you remind me which topic you would like to know more about? For example: passwords, phishing, scams, or privacy.";
            }// end of if statement

            // -----------------------------------------------
            // GENERAL CONVERSATION
            // -----------------------------------------------
            if (input.Contains("how are you"))
            {// start of if statement
                return "I'm doing well, thanks for asking! I'm ready to help you stay safe online.";
            }// end of if statement

            if (input.Contains("purpose") || input.Contains("what can you do") || input.Contains("what can i ask"))
            {// start of if statement
                return "My purpose is to help you learn how to stay safer online. Ask me about passwords, phishing, scams, privacy, safe browsing, malware, 2FA, VPNs, or firewalls! You can also type 'add task', 'view tasks', or 'start quiz'.";
            }// end of if statement

            if (input.Contains("hello") || input.Contains("hi") || input.Contains("hey"))
            {// start of if statement

                // personalise using memory
                if (favouriteTopic != "")
                {
                    return "Hello " + userName + "! As someone interested in " + favouriteTopic + ", would you like another tip on it?";
                }
                return "Hello " + userName + "! How can I help you stay safe online today?";

            }// end of if statement

            // -----------------------------------------------
            // MEMORY AND RECALL
            // -----------------------------------------------
            if (input.Contains("i'm interested in") || input.Contains("i am interested in") || input.Contains("i like"))
            {// start of if statement

                foreach (var entry in keywordResponses)
                {// start of foreach loop
                    if (input.Contains(entry.Key.ToLower()))
                    {// start of if statement
                        favouriteTopic = entry.Key;
                        lastTopic = entry.Key;
                        log.add_entry("Memory: Favourite topic set to '" + entry.Key + "'.");
                        return "Great! I'll remember that you're interested in " + entry.Key + ". It's a crucial part of staying safe online. " + GetRandomResponse(entry.Key);
                    }// end of if statement
                }// end of foreach loop

            }// end of if statement

            // -----------------------------------------------
            // TASK 3 - NLP: "remind me to update my password"
            // -----------------------------------------------
            if (input.Contains("remind me to") || input.Contains("remind me about") || input.Contains("set a reminder") ||
                input.Contains("remind") || input.Contains("reminder"))
            {// start of if statement

                foreach (var entry in keywordResponses)
                {// start of foreach loop
                    if (input.Contains(entry.Key.ToLower()))
                    {// start of if statement
                        pendingTaskName = entry.Key;
                        pendingTaskDescription = generate_task_description(entry.Key);
                        waitingForReminder = true;
                        log.add_entry("NLP: Reminder request for '" + entry.Key + "'.");
                        return "Reminder noted for '" + entry.Key + "'. Would you like to set a timeframe? (e.g. 'yes, remind me in 3 days') or type 'no' to skip.";
                    }// end of if statement
                }// end of foreach loop

                return "Sure! What would you like a reminder about? Try: 'Remind me about password safety' or 'Remind me about phishing'.";

            }// end of if statement

            // -----------------------------------------------
            // NLP SIMULATION - flexible phrasing
            // -----------------------------------------------
            if (input.Contains("tip") || input.Contains("advice") || input.Contains("help me with"))
            {// start of if statement
                foreach (var entry in keywordResponses)
                {// start of foreach loop
                    if (input.Contains(entry.Key.ToLower()))
                    {// start of if statement
                        lastTopic = entry.Key;
                        log.add_entry("NLP: Tip given on '" + entry.Key + "'.");
                        return GetRandomResponse(entry.Key);
                    }// end of if statement
                }// end of foreach loop
            }// end of if statement

            if (input.Contains("what is") || input.Contains("explain") || input.Contains("tell me about"))
            {// start of if statement
                foreach (var entry in keywordResponses)
                {// start of foreach loop
                    if (input.Contains(entry.Key.ToLower()))
                    {// start of if statement
                        lastTopic = entry.Key;
                        return GetRandomResponse(entry.Key);
                    }// end of if statement
                }// end of foreach loop
            }// end of if statement

            // -----------------------------------------------
            // KEYWORD RECOGNITION - direct match
            // -----------------------------------------------
            foreach (var entry in keywordResponses)
            {// start of foreach loop
                if (input.Contains(entry.Key.ToLower()))
                {// start of if statement

                    lastTopic = entry.Key;

                    // personalise if this is the user's favourite topic
                    if (favouriteTopic != "" && favouriteTopic.ToLower() == entry.Key.ToLower())
                    {
                        return "As someone interested in " + entry.Key + ", here is something useful: " + GetRandomResponse(entry.Key);
                    }

                    return GetRandomResponse(entry.Key);

                }// end of if statement
            }// end of foreach loop

            // -----------------------------------------------
            // ERROR HANDLING - default response
            // -----------------------------------------------
            return "I'm not sure I understand. Could you try rephrasing? You can ask me about passwords, phishing, scams, privacy, safe browsing, or type 'add task', 'view tasks', or 'start quiz'.";

        }// end of GenerateResponse method


        // -------------------------------------------------------
        // TASK 1 - TASK MANAGER METHODS
        // Uses the tasks class from tasks.cs
        // -------------------------------------------------------

        // handles the "add task" command and extracts the task name
        private string handle_add_task(string input)
        {// start of handle_add_task method

            string taskName = "";

            // try to extract the task name from different phrasings - Task 3 NLP
            if (input.Contains("-"))
            {// start of if statement
                string[] parts = input.Split('-');
                if (parts.Length > 1)
                {
                    taskName = parts[1].Trim();
                }
            }// end of if statement
            else if (input.Contains("add task to") || input.Contains("add a task to"))
            {// start of else if statement
                taskName = input.Replace("add a task to", "").Replace("add task to", "").Trim();
            }// end of else if statement
            else
            {// start of else statement
                taskName = input.Replace("add task", "").Replace("create task", "").Replace("new task", "").Trim();
            }// end of else statement

            if (taskName == "")
            {// start of if statement
                return "Sure! What task would you like to add? For example: 'add task - Enable two-factor authentication'";
            }// end of if statement

            string taskDescription = generate_task_description(taskName);

            // store task details while we wait for the reminder reply
            pendingTaskName = taskName;
            pendingTaskDescription = taskDescription;
            waitingForReminder = true;

            log.add_entry("Task being added: '" + taskName + "'.");

            return "Task added with the description: \"" + taskDescription + "\" Would you like a reminder? (e.g. 'yes, remind me in 3 days') or type 'no' to skip.";

        }// end of handle_add_task method


        // generates a relevant description based on keywords in the task name
        private string generate_task_description(string taskName)
        {// start of generate_task_description method

            string lower = taskName.ToLower();

            if (lower.Contains("2fa") || lower.Contains("two-factor") || lower.Contains("two factor"))
                return "Set up two-factor authentication to add an extra layer of security to your accounts.";
            if (lower.Contains("password"))
                return "Review and update your passwords to make sure they are strong and unique.";
            if (lower.Contains("privacy"))
                return "Review account privacy settings to ensure your data is protected.";
            if (lower.Contains("vpn"))
                return "Set up a VPN to protect your browsing on public Wi-Fi networks.";
            if (lower.Contains("antivirus") || lower.Contains("malware"))
                return "Run an antivirus scan to check for any malware or threats on your device.";
            if (lower.Contains("backup"))
                return "Back up your important files to prevent data loss from ransomware or hardware failure.";
            if (lower.Contains("firewall"))
                return "Check that your firewall is enabled and configured correctly.";
            if (lower.Contains("phishing"))
                return "Learn to identify phishing emails to avoid falling victim to online scams.";

            return "Complete the cybersecurity task: " + taskName + ".";

        }// end of generate_task_description method


        // handles the user's reply when asked if they want a reminder
        // uses the same Regex and DateTime logic from your tasks_demo project
        private void handle_reminder_reply(string input)
        {// start of handle_reminder_reply method

            string lower = input.ToLower().Trim();

            if (lower == "no" || lower == "no thanks" || lower == "skip" || lower == "nope")
            {// start of if statement

                // save task with no reminder - calls insert_task in tasks.cs
                manage_task.insert_task(pendingTaskName, pendingTaskDescription, "no reminder", "pending");
                waitingForReminder = false;
                log.add_entry("Task saved: '" + pendingTaskName + "' (no reminder).");
                chats_list.Items.Add("Bot: Got it! Task '" + pendingTaskName + "' has been saved with no reminder.");

            }// end of if statement
            else if (lower.StartsWith("yes") || lower.Contains("remind me in") || lower.Contains("days"))
            {// start of else if statement

                // use Regex to pull out the number - same as your tasks_demo project
                string days_number = Regex.Replace(lower, @"[^0-9]", "");

                if (days_number != "")
                {// start of if statement

                    // cast and add to current date - same as your tasks_demo project
                    double days = int.Parse(days_number);
                    DateTime user_reminder = DateTime.Now.AddDays(days);
                    string format_date = user_reminder.ToString("MMMM dd yyyy");

                    // save task with reminder - calls insert_task in tasks.cs
                    manage_task.insert_task(pendingTaskName, pendingTaskDescription, format_date, "pending");
                    waitingForReminder = false;
                    log.add_entry("Reminder set: '" + pendingTaskName + "' on " + format_date + ".");
                    chats_list.Items.Add("Bot: good, i will remind you in " + days + " days on the " + format_date);

                }// end of if statement
                else
                {// start of else statement
                    chats_list.Items.Add("Bot: Please specify how many days, e.g. 'yes, remind me in 3 days'.");
                    return;
                }// end of else statement

            }// end of else if statement
            else
            {// start of else statement
                chats_list.Items.Add("Bot: Please say 'yes, remind me in X days' or 'no' to skip.");
                return;
            }// end of else statement

            pendingTaskName = "";
            pendingTaskDescription = "";

            chats_list.ScrollIntoView(chats_list.Items[chats_list.Items.Count - 1]);

        }// end of handle_reminder_reply method


        // opens the tasks page and loads tasks from the database
        private string open_tasks_page()
        {// start of open_tasks_page method

            chats_grid.Visibility = Visibility.Hidden;
            tasks_grid.Visibility = Visibility.Visible;
            auto_load_tasks();
            log.add_entry("User opened task manager.");
            return "";

        }// end of open_tasks_page method


        // marks a task as done via chat command - calls update_taskStatus in tasks.cs
        private string handle_complete_task(string input)
        {// start of handle_complete_task method

            string numbers = Regex.Replace(input, @"[^0-9]", "");

            if (numbers == "")
            {// start of if statement
                return "Please include the task number. For example: 'complete task 1'.";
            }// end of if statement

            int id = int.Parse(numbers);
            manage_task.update_taskStatus(id);
            log.add_entry("Task " + id + " marked as done.");
            return "Task " + id + " has been marked as done. Well done!";

        }// end of handle_complete_task method


        // deletes a task via chat command - calls delete_task in tasks.cs
        private string handle_delete_task(string input)
        {// start of handle_delete_task method

            string numbers = Regex.Replace(input, @"[^0-9]", "");

            if (numbers == "")
            {// start of if statement
                return "Please include the task number. For example: 'delete task 1'.";
            }// end of if statement

            int id = int.Parse(numbers);
            manage_task.delete_task(id);
            log.add_entry("Task " + id + " deleted.");
            return "Task " + id + " has been deleted.";

        }// end of handle_delete_task method


        // -------------------------------------------------------
        // TASK GRID BUTTON HANDLERS
        // Same pattern as your tasks_demo project
        // -------------------------------------------------------

        // goes back to the chat from the tasks page - same as back_to_chats in tasks_demo
        private void back_to_chat(object sender, RoutedEventArgs e)
        {// start of back_to_chat method

            tasks_grid.Visibility = Visibility.Hidden;
            chats_grid.Visibility = Visibility.Visible;

        }// end of back_to_chat method


        // reloads the task list - same as autoLoad_task in tasks_demo
        private void refresh_tasks(object sender, RoutedEventArgs e)
        {// start of refresh_tasks method

            auto_load_tasks();

        }// end of refresh_tasks method


        // opens tasks page from the View Tasks button click
        private void view_tasks_button(object sender, RoutedEventArgs e)
        {// start of view_tasks_button method

            chats_grid.Visibility = Visibility.Hidden;
            tasks_grid.Visibility = Visibility.Visible;
            auto_load_tasks();
            log.add_entry("User opened task manager.");

        }// end of view_tasks_button method


        // double click on a task to mark it done or delete it - same as manage_tasks in tasks_demo
        private void manage_tasks(object sender, MouseButtonEventArgs e)
        {// start of manage_tasks method

            if (view_tasks.SelectedValue == null)
            {
                return;
            }

            // get the selected task text - same as tasks_demo
            string get_selected_value = view_tasks.SelectedValue.ToString();

            // get the task id from the start of the string - same as tasks_demo
            string get_id = get_selected_value.Substring(0, 1);
            int id = int.Parse(get_id);

            // if task is done, delete it. if pending, mark it done - same as tasks_demo
            if (get_selected_value.ToLower().EndsWith("done"))
            {// start of if statement
                manage_task.delete_task(id);
                log.add_entry("Task " + id + " deleted from task manager.");
            }// end of if statement
            else
            {// start of else statement
                manage_task.update_taskStatus(id);
                log.add_entry("Task " + id + " marked as done from task manager.");
            }// end of else statement

            // reload the list - same as tasks_demo
            auto_load_tasks();

        }// end of manage_tasks method


        // clears and reloads the task list - same as autoLoad_task in tasks_demo
        private void auto_load_tasks()
        {// start of auto_load_tasks method

            view_tasks.Items.Clear();
            manage_task.load_tasks(view_tasks);

        }// end of auto_load_tasks method


        // -------------------------------------------------------
        // TASK 2 - QUIZ METHODS
        // Uses the quiz class from quiz.cs
        // -------------------------------------------------------

        // starts the quiz - calls start_quiz in quiz.cs
        private string start_quiz()
        {// start of start_quiz method

            cyber_quiz.start_quiz();
            log.add_entry("Quiz started by " + userName + ".");
            show_quiz_question();
            return "Starting the Cybersecurity Quiz! There are " + cyber_quiz.questions.Count + " questions. Good luck!";

        }// end of start_quiz method


        // shows the current quiz question in the chat
        private void show_quiz_question()
        {// start of show_quiz_question method

            if (cyber_quiz.is_finished())
            {// start of if statement
                end_quiz();
                return;
            }// end of if statement

            List<string> q = cyber_quiz.get_current_question();

            chats_list.Items.Add("Bot: Question " + (cyber_quiz.currentQuestionIndex + 1) + " of " + cyber_quiz.questions.Count + ":");
            chats_list.Items.Add("Bot: " + q[0]);

            // show only non-empty options (true/false only has A and B)
            for (int i = 1; i <= 4; i++)
            {// start of for loop
                if (q[i] != "")
                {
                    chats_list.Items.Add("Bot: " + q[i]);
                }
            }// end of for loop

            chats_list.Items.Add("Bot: Type A, B, C, or D to answer.");
            chats_list.ScrollIntoView(chats_list.Items[chats_list.Items.Count - 1]);

        }// end of show_quiz_question method


        // checks the user's answer - calls check_answer in quiz.cs
        private void handle_quiz_answer(string input)
        {// start of handle_quiz_answer method

            string answer = input.Trim().ToUpper();

            if (answer != "A" && answer != "B" && answer != "C" && answer != "D")
            {// start of if statement
                chats_list.Items.Add("Bot: Please type A, B, C, or D to answer.");
                return;
            }// end of if statement

            List<string> q = cyber_quiz.get_current_question();
            string explanation = q[6];

            if (cyber_quiz.check_answer(answer))
            {// start of if statement
                cyber_quiz.score++;
                chats_list.Items.Add("Bot: Correct! " + explanation);
            }// end of if statement
            else
            {// start of else statement
                chats_list.Items.Add("Bot: Incorrect. The correct answer was " + q[5] + ". " + explanation);
            }// end of else statement

            // move to next question - calls next_question in quiz.cs
            cyber_quiz.next_question();

            if (cyber_quiz.is_finished())
            {// start of if statement
                end_quiz();
            }// end of if statement
            else
            {// start of else statement
                show_quiz_question();
            }// end of else statement

            chats_list.ScrollIntoView(chats_list.Items[chats_list.Items.Count - 1]);

        }// end of handle_quiz_answer method


        // ends the quiz and shows the score - calls get_score_feedback in quiz.cs
        private void end_quiz()
        {// start of end_quiz method

            cyber_quiz.quizActive = false;
            chats_list.Items.Add("Bot: Quiz complete! You scored " + cyber_quiz.score + " out of " + cyber_quiz.questions.Count + ".");
            chats_list.Items.Add("Bot: " + cyber_quiz.get_score_feedback());
            log.add_entry("Quiz completed. Score: " + cyber_quiz.score + "/" + cyber_quiz.questions.Count + ".");
            chats_list.ScrollIntoView(chats_list.Items[chats_list.Items.Count - 1]);

        }// end of end_quiz method


        // -------------------------------------------------------
        // SENTIMENT DETECTION - Requirement 6
        // -------------------------------------------------------
        private void DetectAndShowSentiment(string input)
        {// start of DetectAndShowSentiment method

            string lower = input.ToLower();

            // words that suggest a positive mood
            List<string> positiveWords = new List<string>
            { "thank", "thanks", "great", "awesome", "helpful", "good", "nice", "love", "excellent", "amazing" };

            // words that suggest the user is worried or in trouble
            List<string> negativeWords = new List<string>
            { "worried", "scared", "hacked", "attacked", "compromised", "lost", "stolen", "bad", "terrible", "help" };

            // words that suggest the user is confused or frustrated
            List<string> frustratedWords = new List<string>
            { "confused", "don't understand", "cant", "can't", "frustrated", "annoyed", "unclear", "still don't" };

            if (positiveWords.Any(w => lower.Contains(w)))
            {// start of if statement
                chats_list.Items.Add("Bot: That's great to hear! Let me know if there's anything else I can help you with.");
            }// end of if statement
            else if (frustratedWords.Any(w => lower.Contains(w)))
            {// start of else if statement
                chats_list.Items.Add("Bot: It sounds like you might be confused. Let me try to explain more clearly. What topic would you like more details on?");
            }// end of else if statement
            else if (negativeWords.Any(w => lower.Contains(w)))
            {// start of else if statement
                chats_list.Items.Add("Bot: I can see you're concerned — don't worry, I'm here to help. Tell me more about what happened so I can guide you.");
            }// end of else if statement

        }// end of DetectAndShowSentiment method


        // picks a random response for a keyword
        private string GetRandomResponse(string keyword)
        {// start of GetRandomResponse method

            List<string> responses = keywordResponses[keyword];
            int index = random.Next(responses.Count);
            return responses[index];

        }// end of GetRandomResponse method

    }//end of class

}// end of namespace