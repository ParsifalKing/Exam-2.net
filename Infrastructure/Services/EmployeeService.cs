using Dapper;
using Domain.Models;
using Infrastructure.DataContext;

namespace Infrastructure.Services;

public class EmployeeService
{
    private readonly DapperContext _context;

    public EmployeeService()
    {
        _context = new DapperContext();
    }

    public async Task<string> Add(Employee some)
    {
        var sql1 = @"insert into employees(Name,Department) values(@Name,@Department)";
        await _context.Connection().ExecuteAsync(sql1, some);
        return $"Added succesfully!!!";
    }

    public async Task<string> Delete(int id)
    {
        var sql1 = @"delete from Employees where EmployeeId=@EmployeeId";
        await _context.Connection().ExecuteAsync(sql1, new { EmployeeId = id });
        return $"Deleted succesfully!!!";
    }

    public async Task<List<Employee>> Get()
    {
        var sql = @"select * from Employees";
        var result = await _context.Connection().QueryAsync<Domain.Models.Employee>(sql);
        return result.ToList();
    }

    public async Task<string> Update(Employee some)
    {
        var sql = @"update Employees set Name=@Name,Department=@Department where EmployeeId=@EmployeeId ";
        await _context.Connection().ExecuteAsync(sql, some);
        return $"Updated successfully!";
    }

    public async Task<ListOfSome<Employee, Domain.Models.Task>> GetEmployeeTasks(int employeeId)
    {
        var sql = @" select * from Employees where EmployeeId=@EmployeeId;
        select * from Tasks where AssignedTo=@EmployeeId;
        ";

        using (var multiple = _context.Connection().QueryMultiple(sql, new { EmployeeId = employeeId }))
        {
            var employeeTasks = new ListOfSome<Employee, Domain.Models.Task>();
            employeeTasks.Any = multiple.ReadFirst<Employee>();
            employeeTasks.SomeList = multiple.Read<Domain.Models.Task>().ToList();
            return employeeTasks;
        }
    }
}
