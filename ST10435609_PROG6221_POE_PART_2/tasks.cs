using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace ST10435609_PROG6221_POE_PART_2
{//start of namespace

    public class tasks
    {//start of class

        // connection string updated to match your actual database
        // from your SQL query: database = prog_task, table = demo_tasks
        string connection = @"Data Source=(localdb)\MSSQLLocalDB;Database=prog_task;Integrated Security=True";


        // method to test the connection
        public void test_connection()
        {//start of test_connection method

            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("Connection to the database is successful!");
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: " + error.Message);
                }
            }

        }//end of test_connection method


        // method to insert a task into the database
        public void insert_task(string name, string description, string dueDate, string status)
        {//start of insert_task method

            using (SqlConnection connects = new SqlConnection(connection))
            {
                try
                {
                    connects.Open();

                    // using the same demo_tasks table from your SQL script
                    string query = $"insert into demo_tasks (task_name, task_description, task_dueDate, task_status) values ('{name}', '{description}', '{dueDate}', '{status}')";

                    SqlCommand run_query = new SqlCommand(query, connects);
                    run_query.ExecuteNonQuery();
                    connects.Close();

                    MessageBox.Show("Task added successfully!");
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error: " + error.Message);
                }
            }

        }//end of insert_task method


        // method to load all tasks from the database and show them in a ListView
        public void load_tasks(ListView view_tasks)
        {//start of load_tasks method

            SqlConnection connects = new SqlConnection(connection);
            connects.Open();

            string query = $"select * from demo_tasks";
            SqlCommand run_query = new SqlCommand(query, connects);
            SqlDataReader data_collect = run_query.ExecuteReader();

            bool data_found = false;

            while (data_collect.Read())
            {//start of while loop

                data_found = true;

                string task_id = data_collect["task_id"].ToString();
                string task_name = data_collect["task_name"].ToString();
                string task_description = data_collect["task_description"].ToString();
                string task_dueDate = data_collect["task_dueDate"].ToString();
                string task_status = data_collect["task_status"].ToString();

                view_tasks.Items.Add($"{task_id} | Task Name: {task_name} | Task Description: {task_description} | Task Due Date: {task_dueDate} | Task Status: {task_status}");

            }//end of while loop

            if (!data_found)
            {
                view_tasks.Items.Add("No tasks found in the database.");
            }

            connects.Close();

        }//end of load_tasks method


        // method to mark a task as done
        public void update_taskStatus(int id)
        {//start of update_taskStatus method

            SqlConnection connects = new SqlConnection(connection);
            connects.Open();

            string query = $"update demo_tasks set task_status='done' where task_id={id}";
            SqlCommand run_query = new SqlCommand(query, connects);
            run_query.ExecuteNonQuery();

            connects.Close();

        }//end of update_taskStatus method


        // method to delete a task
        public void delete_task(int id)
        {//start of delete_task method

            SqlConnection connects = new SqlConnection(connection);
            connects.Open();

            string query = $"delete from demo_tasks where task_id={id}";
            SqlCommand run_query = new SqlCommand(query, connects);
            run_query.ExecuteNonQuery();

            connects.Close();

        }//end of delete_task method


    }//end of class

}//end of namespace