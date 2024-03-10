using Domain.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController
{
    private readonly EmployeeService _employeeService;

    public EmployeeController()
    {
        _employeeService = new EmployeeService();
    }

    [HttpPost("add-employee")]
    public async Task<string> Add(Employee some)
    {
        return await _employeeService.Add(some);
    }

    [HttpDelete("delete-employee")]
    public async Task<string> Delete(int id)
    {
        return await _employeeService.Delete(id);
    }

    [HttpGet("get-employees")]
    public async Task<List<Employee>> Get()
    {
        return await _employeeService.Get();
    }

    [HttpPut("update-employee")]
    public async Task<string> Update(Employee some)
    {
        return await _employeeService.Update(some);
    }

    [HttpGet("get-employee-tasks")]
    public async Task<ListOfSome<Employee, Domain.Models.Task>> GetEmployeeTasks(int employeeId)
    {
        return await _employeeService.GetEmployeeTasks(employeeId);
    }
}
