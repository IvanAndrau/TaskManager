﻿using System.Text.RegularExpressions;
using TaskManagerAPI.EF;
using TaskManagerAPI.ViewModel;

namespace TaskManagerAPI.AppServices;
using Task = TaskManagerAPI.EF.Task;
public class TaskService (TaskMgrContext context)
{

    public TaskModel AddTask(TaskModel task)
    {
        var dbTask = new Task();
        dbTask.Name = task.Name;
        dbTask.Id = task.Id;
        dbTask.Description = task.Description;
        dbTask.ActionDate = task.ActionDate;
        dbTask.GroupId = task.GroupId;
        context.Tasks.Add(dbTask);
        context.SaveChanges();
        return task;
}

    public IEnumerable<TaskModel> GetTasksByGroupId(Guid groupId)
    {
        var dbTasks = context.Tasks.Where(x => x.GroupId == groupId);
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

    public TaskModel GetTaskById(Guid id)
    {
        var task = context.Tasks.FirstOrDefault(x => x.Id == id);
        return new TaskModel()
        {
            Id = task.Id,
            Name = task.Name,
            Description = task.Description,
            ActionDate = task.ActionDate,
            GroupId = task.GroupId,
        };
    }

    public TaskModel UpdateTask(TaskModel task)
    {
        var dbTask = context.Tasks.FirstOrDefault(x => x.Id == task.Id);
        if (dbTask == null)
        {
            throw new InvalidOperationException();
        }
        dbTask.Name = task.Name;
        dbTask.Description = task.Description;
        dbTask.ActionDate = task.ActionDate;
        dbTask.GroupId = task.GroupId;
        context.SaveChanges();

        return task;
    }

    public void DeleteTask(Guid id)
    {
        try
        {
            var dbTask = context.Tasks.FirstOrDefault(x => x.Id == id);

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
