using Domain.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController
{
    private readonly TaskService _taskService;

    public TaskController()
    {
        _taskService = new TaskService();
    }

    [HttpPost("add-task")]
    public async Task<string> Add(Domain.Models.Task some)
    {
        return await _taskService.Add(some);
    }

    [HttpDelete("delete-task")]
    public async Task<string> Delete(int id)
    {
        return await _taskService.Delete(id);
    }

    [HttpGet("get-tasks")]
    public async Task<List<Domain.Models.Task>> Get()
    {
        return await _taskService.Get();
    }

    [HttpPut("update-task")]
    public async Task<string> Update(Domain.Models.Task some)
    {
        return await _taskService.Update(some);
    }
}
