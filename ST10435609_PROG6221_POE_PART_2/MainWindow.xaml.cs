using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ST10435609_PROG6221_POE_PART_2
{// start of namespace

    public partial class MainWindow : Window
    {   //start of class

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


        public MainWindow()
        {//start of constructor
            InitializeComponent();

            // creating an instance for the class greet_user with a constructor
            new greet_user();

        }// end of constructor


        private void start_ai(object sender, RoutedEventArgs e)
        {// start of method

            // set the logo page grid to be invisible
            logo_grid.Visibility = Visibility.Hidden;

            // set the username page grid to be visible
            username_grid.Visibility = Visibility.Visible;

        }// end of start_ai method


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

            }// end of if statement

            else
            {// start of else statement

                // show message
                MessageBox.Show("Please enter your username to continue!");

            }//end of else statement

        }// end of submit_name method


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

            // check the mood of the user and respond accordingly
            // Requirement 6 - Sentiment Detection
            DetectAndShowSentiment(input);

            // generate a response and add it to the chat list
            string response = GenerateResponse(input.ToLower());
            chats_list.Items.Add("Bot: " + response);

            // scroll to the latest message so the user can see it
            chats_list.ScrollIntoView(chats_list.Items[chats_list.Items.Count - 1]);

        }// end of send_question method


        // this method figures out what the user is asking and returns the right response
        private string GenerateResponse(string input)
        {// start of GenerateResponse method

            // check if the user wants more info on the last topic they asked about
            if (moreTriggers.Any(t => input.Contains(t)))
            {// start of if statement
                if (lastTopic != "" && keywordResponses.ContainsKey(lastTopic))
                {
                    return GetRandomResponse(lastTopic);
                }
                return "Sure! Could you remind me which topic you would like to know more about? For example: passwords, phishing, scams, or privacy.";
            }// end of if statement

            // check for general conversational inputs
            if (input.Contains("how are you"))
            {// start of if statement
                return "I'm doing well, thanks for asking! I'm ready to help you stay safe online.";
            }// end of if statement

            if (input.Contains("purpose") || input.Contains("what can you do") || input.Contains("what can i ask"))
            {// start of if statement
                return "My purpose is to help you learn how to stay safer online. Ask me about passwords, phishing, scams, privacy, safe browsing, malware, 2FA, VPNs, or firewalls!";
            }// end of if statement

            if (input.Contains("hello") || input.Contains("hi") || input.Contains("hey"))
            {// start of if statement

                // use memory to personalise the greeting if we know their favourite topic
                // Memory and Recall
                if (favouriteTopic != "")
                {
                    return "Hello " + userName + "! As someone interested in " + favouriteTopic + ", would you like another tip on it?";
                }

                return "Hello " + userName + "! How can I help you stay safe online today?";

            }// end of if statement

            // check if the user is telling us their favourite topic
            // Memory and Recall
            if (input.Contains("i'm interested in") || input.Contains("i am interested in") || input.Contains("i like"))
            {// start of if statement

                foreach (var entry in keywordResponses)
                {// start of foreach loop
                    if (input.Contains(entry.Key.ToLower()))
                    {// start of if statement
                        favouriteTopic = entry.Key;
                        lastTopic = entry.Key;
                        return "Great! I'll remember that you're interested in " + entry.Key + ". It's a crucial part of staying safe online. " + GetRandomResponse(entry.Key);
                    }// end of if statement
                }// end of foreach loop

            }// end of if statement

            // check if the user wants a reminder about a topic
            // NLP Simulation
            if (input.Contains("remind") || input.Contains("reminder"))
            {// start of if statement

                foreach (var entry in keywordResponses)
                {// start of foreach loop
                    if (input.Contains(entry.Key.ToLower()))
                    {// start of if statement
                        lastTopic = entry.Key;
                        return "Got it! Here is a tip to help you remember about " + entry.Key + ": " + GetRandomResponse(entry.Key);
                    }// end of if statement
                }// end of foreach loop

                return "Sure! What would you like a reminder about? Try: 'Remind me about password safety' or 'Remind me about phishing'.";

            }// end of if statement

            // check if the user is asking for a tip or advice
            // NLP Simulation
            if (input.Contains("tip") || input.Contains("advice") || input.Contains("help me with"))
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

            // check if the user is asking what something is or wants an explanation
            // NLP Simulation
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

            // direct keyword check - Keyword Recognition
            foreach (var entry in keywordResponses)
            {// start of foreach loop
                if (input.Contains(entry.Key.ToLower()))
                {// start of if statement

                    // save the topic so we can use it for follow up questions
                    lastTopic = entry.Key;

                    // if this is the user's favourite topic, personalise the response
                    // Memory and Recall
                    if (favouriteTopic != "" && favouriteTopic.ToLower() == entry.Key.ToLower())
                    {
                        return "As someone interested in " + entry.Key + ", here is something useful: " + GetRandomResponse(entry.Key);
                    }

                    //Random Responses
                    return GetRandomResponse(entry.Key);

                }// end of if statement
            }// end of foreach loop

            // if nothing matched, return a default response
            // Error Handling
            return "I'm not sure I understand. Could you try rephrasing? You can ask me about passwords, phishing, scams, privacy, or safe browsing.";

        }// end of GenerateResponse method


        // this method checks the mood of the user based on keywords in their message
        // Sentiment Detection
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


        // this method picks a random response from the list for a given keyword
        // Random Responses
        private string GetRandomResponse(string keyword)
        {// start of GetRandomResponse method

            List<string> responses = keywordResponses[keyword];
            int index = random.Next(responses.Count);
            return responses[index];

        }// end of GetRandomResponse method

    }//end of class

}// end of namespace