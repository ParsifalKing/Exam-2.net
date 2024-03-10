using Dapper;
using Domain.Models;
using Infrastructure.DataContext;

namespace Infrastructure.Services;

public class ProjectService
{
    private readonly DapperContext _context;

    public ProjectService()
    {
        _context = new DapperContext();
    }

    public async Task<string> Add(Project some)
    {
        var sql1 = @"insert into Projects(Name,Description,StartDate,EndDate) values(@Name,@Description,@StartDate,@EndDate)";
        await _context.Connection().ExecuteAsync(sql1, some);
        return $"Added succesfully!!!";
    }

    public async Task<string> Delete(int id)
    {
        var sql1 = @"delete from Projects where ProjectId=@ProjectId";
        await _context.Connection().ExecuteAsync(sql1, new { ProjectId = id });
        return $"Deleted succesfully!!!";
    }

    public async Task<List<Project>> Get()
    {
        var sql = @"select * from Projects";
        var result = await _context.Connection().QueryAsync<Project>(sql);
        return result.ToList();
    }

    public async Task<string> Update(Project some)
    {
        var sql = @"update Projects set Name=@Name,Description=@Description,StartDate=@StartDate,EndDate=@EndDate where ProjectId=@ProjectId ";
        await _context.Connection().ExecuteAsync(sql, some);
        return $"Updated successfully!";
    }

    public async Task<List<ListOfSome<Project, Domain.Models.TaskType>>> GetProjectsTasks()
    {
        var sql1 = @" select * from Projects;";
        var result = await _context.Connection().QueryAsync<int>(sql1);
        var projects_id = result.ToList();

        var sql2 = @" select * from Projects where ProjectId=@ProjectId;
        select * from Tasks where ProjectId=@ProjectId;
        ";

        var employeesTasks = new List<ListOfSome<Project, Domain.Models.TaskType>>();
        foreach (var item in projects_id)
        {
            using (var multiple = _context.Connection().QueryMultiple(sql2, new { ProjectId = item }))
            {
                var employeeTasks = new ListOfSome<Project, Domain.Models.TaskType>();
                employeeTasks.Any = multiple.ReadFirst<Project>();
                employeeTasks.SomeList = multiple.Read<Domain.Models.TaskType>().ToList();
                foreach (var item2 in employeeTasks.SomeList)
                {
                    item2.TasksCount = _context.Connection().QueryFirst<int>(@"select Count(TaskId) from Tasks where ProjectId=@ProjectId", new { ProjectId = item });
                    item2.FinishedTasksCount = _context.Connection().QueryFirst<int>(@"select Count(TaskId) from Tasks where Status = 'finished' and ProjectId=@ProjectId ", new { ProjectId = item });
                }
                employeesTasks.Add(employeeTasks);
            }
        }
        return employeesTasks;
    }


    public async Task<ListOfSome<Project, Domain.Models.Employee>> GetProjectEmployees(int projectId)
    {
        var sql = @" select * from Projects where ProjectId=@ProjectId;
        select AssignedTo from Tasks where ProjectId=@ProjectId; 
        ";

        using (var multiple = _context.Connection().QueryMultiple(sql, new { ProjectId = projectId }))
        {
            var employeeTasks = new ListOfSome<Project, Domain.Models.Employee>();
            employeeTasks.Any = multiple.ReadFirst<Project>();
            var employeesId = multiple.Read<int>().ToList();
            foreach (var item in employeesId)
            {
                var sql1 = @" select * from Employees where EmployeeId=@EmployeeId;";
                var employee = _context.Connection().QueryFirst<Employee>(sql1, new { EmployeeId = item });
                var employees = new List<Employee>();
                employees.Add(employee);
                employeeTasks.SomeList = employees;
            }
            return employeeTasks;
        }
    }


    public async Task<ListOfSome<Project, Domain.Models.Task>> GetProjectTasks(int projectId)
    {
        var sql = @" select * from Projects where ProjectId=@ProjectId;
        select * from Tasks where ProjectId=@ProjectId;
        ";

        var employeesTasks = new ListOfSome<Project, Domain.Models.Task>();

        using (var multiple = _context.Connection().QueryMultiple(sql, new { ProjectId = projectId }))
        {
            var projectTasks = new ListOfSome<Project, Domain.Models.Task>();
            projectTasks.Any = multiple.ReadFirst<Project>();
            projectTasks.SomeList = multiple.Read<Domain.Models.Task>().ToList();
            return projectTasks;
        }
    }

}
