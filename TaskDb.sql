
create table Employees
(
    EmployeeId serial primary key,
	Name varchar(100),
	Department varchar(200)
);

create table Projects
(
    ProjectId serial primary key,
	Name varchar(100),
	Description text,
	StartDate date,
	EndDate date
);

create table Tasks
(
    TaskId serial primary key,
	ProjectId int references Projects(ProjectId),
	Title varchar(200),
	Desription text,
	AssignedTo int references Employees(EmployeeId),
	DueDate date,
	Status varchar(50)
);
