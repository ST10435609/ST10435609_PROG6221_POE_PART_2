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
{// start of nam

    public partial class MainWindow : Window
    {   //start of class
        public MainWindow()
        {//start of constructor
            InitializeComponent();

            //creating an instance for the class greet_user
            //with a constructor

            new greet_user();

        }// end of constructor

        private void start_ai(object sender, RoutedEventArgs e)
        {// start of method

            //set the logo page grid to be invisible
            logo_grid.Visibility = Visibility.Hidden;

            //set the username page grid to be visible
            username_grid.Visibility = Visibility.Visible;

        }// end of sart_ai method


        //i added this one, so it can be removed if not needed, but i think it will be needed to submit the name of the user
        private void submit_name(object sender, RoutedEventArgs e)
        {// start of submit_name method

            //collect the username from the text box
            string collect_username = user_name.Text.ToString();

            //check if the name is  empty or not
            if (collect_username !="") 
            { // start of if statment

                //show message
                MessageBox.Show("Welcome " + collect_username + " to the awareness AI chatbot!");

                //set the username page grid to be visible
                username_grid.Visibility = Visibility.Hidden;

                // set the chats page grid to be visible
                chats_grid.Visibility = Visibility.Visible;

                //add the username to the welcome message in the chats page
                chats_list.Items.Add("Welcome " + collect_username + " to the awareness AI chatbot! How can I assist you today?");

            }// end of if statemnt

            else
            {// start of else statement
                //show message
                MessageBox.Show("Please enter your username to continue!");
            }//end of else statement



        }// end of submit_name method

        private void send_question(object sender, RoutedEventArgs e)
        {// start of send_question method


        }// end of send_question method

    }//end of class


}// end of namespace
