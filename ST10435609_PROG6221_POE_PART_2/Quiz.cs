using System.Collections.Generic;

namespace ST10435609_PROG6221_POE_PART_2
{//start of namespace

    public class quiz
    {//start of class

        // each question is stored as a List<string>:
        // [0] = question text
        // [1] = option A
        // [2] = option B
        // [3] = option C  (empty string for true/false questions)
        // [4] = option D  (empty string for true/false questions)
        // [5] = correct answer letter
        // [6] = explanation shown after answering
        public List<List<string>> questions = new List<List<string>>();

        // tracks which question the user is currently on
        public int currentQuestionIndex = 0;

        // tracks how many correct answers the user has given
        public int score = 0;

        // flag to check if the quiz is currently running
        public bool quizActive = false;


        public quiz()
        {//start of constructor

            // load all the questions when a quiz object is created
            setup_questions();

        }//end of constructor


        // method to set up all the quiz questions
        private void setup_questions()
        {//start of setup_questions method

            questions = new List<List<string>>
            {
                // question 1 - multiple choice
                new List<string> {
                    "What should you do if you receive an email asking for your password?",
                    "A) Reply with your password",
                    "B) Delete the email",
                    "C) Report the email as phishing",
                    "D) Ignore it",
                    "C",
                    "Correct! Reporting phishing emails helps prevent scams and protects others."
                },
                // question 2 - true or false
                new List<string> {
                    "True or False: Using the same password for all accounts is safe.",
                    "A) True",
                    "B) False",
                    "",
                    "",
                    "B",
                    "False! If one account is hacked, all your other accounts become at risk too."
                },
                // question 3 - multiple choice
                new List<string> {
                    "What does 2FA stand for?",
                    "A) Two-Factor Authentication",
                    "B) Two-File Application",
                    "C) Trusted Firewall Access",
                    "D) Two-Factor Authorisation",
                    "A",
                    "Correct! 2FA adds a second step to verify your identity when logging in."
                },
                // question 4 - true or false
                new List<string> {
                    "True or False: A VPN protects your privacy on public Wi-Fi.",
                    "A) True",
                    "B) False",
                    "",
                    "",
                    "A",
                    "True! A VPN encrypts your traffic so others can't spy on what you're doing."
                },
                // question 5 - multiple choice
                new List<string> {
                    "Which of the following is a sign of a phishing email?",
                    "A) It comes from someone you know",
                    "B) It creates urgency and asks you to click a link",
                    "C) It has your correct full name",
                    "D) It comes from your company domain",
                    "B",
                    "Correct! Urgent requests to click links are a classic phishing tactic."
                },
                // question 6 - true or false
                new List<string> {
                    "True or False: It is safe to download free software from any website.",
                    "A) True",
                    "B) False",
                    "",
                    "",
                    "B",
                    "False! Free downloads from untrusted sites often contain hidden malware."
                },
                // question 7 - multiple choice
                new List<string> {
                    "What is the main purpose of a firewall?",
                    "A) To speed up your internet connection",
                    "B) To block suspicious network traffic",
                    "C) To store your passwords",
                    "D) To encrypt your emails",
                    "B",
                    "Correct! A firewall monitors and blocks potentially harmful network connections."
                },
                // question 8 - multiple choice
                new List<string> {
                    "What is social engineering in cybersecurity?",
                    "A) Building social media apps",
                    "B) Manipulating people into giving up confidential information",
                    "C) Designing better passwords",
                    "D) Engineering secure networks",
                    "B",
                    "Correct! Social engineering tricks people into revealing sensitive information."
                },
                // question 9 - true or false
                new List<string> {
                    "True or False: HTTPS means a website is completely safe to use.",
                    "A) True",
                    "B) False",
                    "",
                    "",
                    "B",
                    "False! HTTPS only means the connection is encrypted, not that the site is safe."
                },
                // question 10 - multiple choice
                new List<string> {
                    "How often should you update your passwords?",
                    "A) Never, once is enough",
                    "B) Every 10 years",
                    "C) Regularly, especially after a data breach",
                    "D) Only when you forget them",
                    "C",
                    "Correct! Regularly updating passwords, especially after breaches, keeps you safer."
                },
                // question 11 - true or false
                new List<string> {
                    "True or False: Antivirus software alone is enough to protect you from all threats.",
                    "A) True",
                    "B) False",
                    "",
                    "",
                    "B",
                    "False! You also need strong passwords, 2FA, and safe browsing habits."
                }
            };

        }//end of setup_questions method


        // method to start the quiz - resets everything
        public void start_quiz()
        {//start of start_quiz method

            quizActive = true;
            currentQuestionIndex = 0;
            score = 0;

        }//end of start_quiz method


        // method to get the current question
        public List<string> get_current_question()
        {//start of get_current_question method

            return questions[currentQuestionIndex];

        }//end of get_current_question method


        // method to check if the user's answer is correct
        // returns true if correct, false if wrong
        public bool check_answer(string answer)
        {//start of check_answer method

            string correct_answer = questions[currentQuestionIndex][5];
            return answer.Trim().ToUpper() == correct_answer;

        }//end of check_answer method


        // method to move to the next question
        public void next_question()
        {//start of next_question method

            currentQuestionIndex++;

        }//end of next_question method


        // method to check if all questions have been answered
        public bool is_finished()
        {//start of is_finished method

            return currentQuestionIndex >= questions.Count;

        }//end of is_finished method


        // method to get the final score feedback message
        public string get_score_feedback()
        {//start of get_score_feedback method

            int total = questions.Count;

            if (score == total)
                return "Perfect score! You're a cybersecurity pro!";
            else if (score >= total * 0.8)
                return "Great job! You really know your cybersecurity!";
            else if (score >= total * 0.5)
                return "Good effort! Keep learning to stay safe online!";
            else
                return "Keep learning to stay safe online! Review the topics and try again.";

        }//end of get_score_feedback method


    }//end of class

}//end of namespace