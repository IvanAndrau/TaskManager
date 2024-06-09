using System.Text.RegularExpressions;
using TaskManagerAPI.EF;
using TaskManagerAPI.ViewModel;

namespace TaskManagerAPI.AppServices;
using Task = TaskManagerAPI.EF.Task;
public class TaskService (TaskMgrContext context)
{

    public TaskModel AddTask(TaskModel task, string userId)
    {
        var dbTask = new Task();
        dbTask.UserId = userId;
        dbTask.Name = task.Name;
        dbTask.Id = Guid.NewGuid();
        dbTask.Description = task.Description;
        dbTask.ActionDate = task.ActionDate;
        dbTask.GroupId = task.GroupId;
        context.Tasks.Add(dbTask);
        context.SaveChanges();
        task.Id = dbTask.Id;
        return task;
    }

    public IEnumerable<TaskModel> GetTasksByGroupId(Guid groupId, string userId)
    {
        var dbTasks = context.Tasks.Where(x => x.GroupId == groupId && x.UserId == userId);
        List<TaskModel> tasks = new List<TaskModel>();
        foreach (var task in dbTasks)
        {
            tasks.Add(new TaskModel()
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                ActionDate = task.ActionDate,
                GroupId = task.GroupId,
            });
        }
        return tasks;
    }

    public TaskModel GetTaskById(Guid id, string userId)
    {
        var task = context.Tasks.FirstOrDefault(x => x.Id == id && x.UserId == userId);
        return new TaskModel()
        {
            Id = task.Id,
            Name = task.Name,
            Description = task.Description,
            ActionDate = task.ActionDate,
            GroupId = task.GroupId,
        };
    }

    public TaskModel UpdateTask(TaskModel task, string userId)
    {
        var dbTask = context.Tasks.FirstOrDefault(x => x.Id == task.Id && x.UserId == userId);
        if (dbTask == null)
        {
            throw new InvalidOperationException();
        }
        dbTask.Name = task.Name;
        dbTask.Description = task.Description;
        dbTask.ActionDate = task.ActionDate;
        dbTask.GroupId = task.GroupId;
        dbTask.UserId = userId;                 //
        context.SaveChanges();

        return task;
    }

    public void DeleteTask(Guid id, string userId)
    {
        try
        {
            var dbTask = context.Tasks.FirstOrDefault(x => x.Id == id && x.UserId == userId);

            if (dbTask == null)
            {
                throw new KeyNotFoundException();
            }
            context.Tasks.Remove(dbTask);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
