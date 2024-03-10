using Domain.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController
{
    private readonly ProjectService _projectService;

    public ProjectController()
    {
        _projectService = new ProjectService();
    }

    [HttpPost("add-project")]
    public async Task<string> Add(Project some)
    {
        return await _projectService.Add(some);
    }

    [HttpDelete("delete-Project")]
    public async Task<string> Delete(int id)
    {
        return await _projectService.Delete(id);
    }

    [HttpGet("get-Projects")]
    public async Task<List<Project>> Get()
    {
        return await _projectService.Get();
    }

    [HttpPut("update-Project")]
    public async Task<string> Update(Project some)
    {
        return await _projectService.Update(some);
    }

    [HttpGet("get-projects-tasks")]
    public async Task<List<ListOfSome<Project, Domain.Models.TaskType>>> GetProjectsTasks()
    {
        return await _projectService.GetProjectsTasks();
    }

    [HttpGet("get-project-employees")]
    public async Task<ListOfSome<Project, Domain.Models.Employee>> GetProjectEmployees(int projectId)
    {
        return await _projectService.GetProjectEmployees(projectId);
    }

    [HttpGet("get-project-tasks")]
    public async Task<ListOfSome<Project, Domain.Models.Task>> GetProjectTasks(int projectId)
    {
        return await _projectService.GetProjectTasks(projectId);
    }

}
