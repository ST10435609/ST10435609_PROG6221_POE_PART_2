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


    }//end of class

}// end of namespace
