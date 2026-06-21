create database prog_task;

use prog_task;

create table demo_tasks(
task_id int primary key identity(1,1),
task_name varchar(100),
task_description varchar(100),
task_dueDate varchar(20),
task_status varchar(20)
);

select * from demo_tasks;