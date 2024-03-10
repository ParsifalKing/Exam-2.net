namespace Domain.Models;

public class TaskType : Task
{
    public int TasksCount { get; set; }
    public int FinishedTasksCount { get; set; }
}
