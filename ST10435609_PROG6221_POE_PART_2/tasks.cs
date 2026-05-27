using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace ST10435609_PROG6221_POE_PART_2
{//start of namespace
    public class tasks
    {//start of class
     //create a method to manage the task

        //global connection string, with varables for decleration
        string connection = @"Data source=(localdb)\task_demo;Database=prog_tasks";

        //create a method to test the connection
        public void test_connection()
        {//start test connection medthod
            /*SqlConnection - used to connect to the database
             * SqlCommand - used to execute sql commands, all of them
             * SqlDataReader - used to read data from the database
             *                  the SqlCommand, and show user data
             *                  
             */

            //connect to the database
            using (SqlConnection conn = new SqlConnection(connection))
            {
                //try to execute the command, if it fails, catch the error and show the user
                try
                {
                    //open the connection and close it, to test if the connection is valid
                    conn.Open();

                    //put the database query and run it
                    MessageBox.Show("Connection to the database is successful!");

                    //the connection will be closed automatically by the using block
                }
                catch (Exception error)
                {
                    //show the user the error
                    MessageBox.Show("Error: " + error.Message);
                }
            }
        }//end of connection test method


        //method too insert a task into the database
        public void insert_task(string name, string description, string dueDate, string status)
        {//start of insert_task method
            //connect to the database
            using (SqlConnection connects = new SqlConnection(connection))
            {
                //try to execute the command, if it fails, catch the error and show the user
                try
                {


                    //open the connection and close it, to test if the connection is valid
                    connects.Open();
                    string query = $"insert into demo_tasks  values ('{name}', '{description}', '{dueDate}', '{status}')";

                    //use the sql command object to execute the query
                    SqlCommand run_query = new SqlCommand(query, connects);
                    //the run the query as a nonExecuteQuery() 
                    run_query.ExecuteNonQuery();
                    connects.Close();


                    MessageBox.Show("Task added successfully!");
                    //the connection will be closed automatically by the using block
                }
                catch (Exception error)
                {
                    //show the user the error
                    MessageBox.Show("Error: " + error.Message);
                }
            }
        }//end of insert_task method

        //method to auto load the tasks from the database and show them to the user
        public void load_tasks(ListView view_tasks)
        {//start of load task method
            /*SqlConnection - used to connect to the database
            * SqlCommand - used to execute sql commands, all of them
            * SqlDataReader - used to read data from the database
            *                  the SqlCommand, and show user data
            *                  
            */

            //create an instance of the connection to the database
            SqlConnection connects = new SqlConnection(connection);

            //open conection
            connects.Open();

            //temp var to hold the query
            string query = $"select * from demo_tasks";

            //use slqcommand to execute the query
            SqlCommand run_query = new SqlCommand(query, connects);

            //reading what the command found and show/display it using SqlDataReader
            SqlDataReader data_collect = run_query.ExecuteReader();

            //temp variable for boolean, ro get the status of the data found or not found, not found = false, found = true
            bool data_found = false;

            while (data_collect.Read())
            {//start of while loop
                //if data is found, then show it to the user
                data_found = true;
                //show the data to the user
                //getting all the columns by their names and showing them to the user, using the ListView control
                string task_id = data_collect["task_id"].ToString();
                string task_name = data_collect["task_name"].ToString();
                string task_description = data_collect["task_description"].ToString();
                string task_dueDate = data_collect["task_dueDate"].ToString();
                string task_status = data_collect["task_status"].ToString();

                view_tasks.Items.Add($"{task_id} | Task Name: {task_name} | Task Description: {task_description} | Task Due Date: {task_dueDate} | Task Status: {task_status}");

            }//end of while loop

            //display message to the user if no data is found
            if (!data_found)
            {
                view_tasks.Items.Add("No tasks found in the database.");
            }


            connects.Close();

        }//end of load task method

        //method to update task
        public void update_taskStatus(int id)
        {//start of update_taskStatus method
            //create connection

            SqlConnection connects = new SqlConnection(connection);

            //then open connection

            connects.Open();
            //then use sql command to run the query
            //temp variable to hold  the query

            string query = $"update demo_tasks set task_status='done' where task_id={id}";
            SqlCommand run_query = new SqlCommand(query, connects);
            run_query.ExecuteNonQuery();


            connects.Close();

        }//end of update_taskStatus method

        //method to delete
        public void delete_task(int id)
        { //start of delete method
          //connect
            SqlConnection connects = new SqlConnection(connection);

            //the open connection
            connects.Open();

            //temp variable to hold query
            string query = $"delete demo_tasks where task_id={id}";
            SqlCommand run_query = new SqlCommand(query, connects);
            run_query.ExecuteNonQuery();

            connects.Close();

        }//end  of delete method


    }//end of class

}//end of namespace