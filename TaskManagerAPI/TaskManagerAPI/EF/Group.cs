namespace TaskManagerAPI.EF;

public class Group
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Task> Tasks { get; set; }
}
