using Dapper;
using Domain.Models;
using Infrastructure.DataContext;

namespace Infrastructure.Services;

public class TaskService : IService<Domain.Models.Task>
{

    private readonly DapperContext _context;

    public TaskService()
    {
        _context = new DapperContext();
    }

    public async Task<string> Add(Domain.Models.Task some)
    {
        var sql1 = @"insert into Tasks(ProjectId,Title,Desription,AssignedTo,DueDate,Status) values(@ProjectId,@Title,@Desription,@AssignedTo,@DueDate,@Status)";
        await _context.Connection().ExecuteAsync(sql1, some);
        return $"Added succesfully!!!";
    }

    public async Task<string> Delete(int id)
    {
        var sql1 = @"delete from Tasks where TaskId=@TaskId";
        await _context.Connection().ExecuteAsync(sql1, new { TaskId = id });
        return $"Deleted succesfully!!!";
    }

    public async Task<List<Domain.Models.Task>> Get()
    {
        var sql = @"select * from Tasks";
        var result = await _context.Connection().QueryAsync<Domain.Models.Task>(sql);
        return result.ToList();
    }

    public async Task<string> Update(Domain.Models.Task some)
    {
        var sql = @"update Tasks set ProjectId=@ProjectId,Title=@Title,Description=@Description,AssignedTo=@AssignedTo,DueDate=@DueDate,Status=@Status where TaskId=@TaskId ";
        await _context.Connection().ExecuteAsync(sql, some);
        return $"Updated successfully!";
    }
}
