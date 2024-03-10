namespace Domain.Models;

public class Task
{
    public int TaskId { get; set; }
    public int ProjectId { get; set; }
    public string Title { get; set; }
    public string Desription { get; set; }
    public int AssignedTo { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; }
}
