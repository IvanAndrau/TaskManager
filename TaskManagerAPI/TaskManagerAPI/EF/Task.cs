namespace TaskManagerAPI.EF;

public class Task 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime ActionDate { get; set; }

    public Guid GroupId { get; set; }

    public Group Group { get; set; }

}
