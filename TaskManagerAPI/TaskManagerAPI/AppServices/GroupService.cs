using System.Text.RegularExpressions;
using TaskManagerAPI.EF;
using TaskManagerAPI.ViewModel;

namespace TaskManagerAPI.AppServices;
using Group = TaskManagerAPI.EF.Group;
public class GroupService (TaskMgrContext context)
{
    public GroupModel AddGroup(GroupModel group, string userId)
    {
        try
        {
            var dbGroup = new Group();
            dbGroup.Name = group.Name;
            dbGroup.Id = Guid.NewGuid();
            dbGroup.UserId = userId;
            context.Groups.Add(dbGroup);
            context.SaveChanges();
            group.Id = dbGroup.Id;
            return group;
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    public IEnumerable<GroupModel> GetAllGroups(string userId)
    {
        var dbGroup = context.Groups.Where(x => x.UserId == userId);
        List<GroupModel> groups = new List<GroupModel>();
        foreach (var group in context.Groups)
        {
            groups.Add(new GroupModel()
            {
                Id = group.Id,
                Name = group.Name
            });
        }
        if (groups.Count == 0) return null;
        return groups;
    }

    public GroupModel GetGroupById(Guid id, string userId)
    {
        var group = context.Groups.FirstOrDefault(x => x.Id == id && x.UserId == userId);
        if (group == null)
        {
            return null;
        }
        return new GroupModel()
        {
            Id = group.Id,
            Name = group.Name
        };
    }

    public GroupModel UpdateGroup(GroupModel group, string userId)
    {
        var dbGroup = context.Groups.FirstOrDefault(x => x.Id == group.Id && x.UserId == userId);
        if(dbGroup == null)
        {
            throw new InvalidOperationException();
        }
        dbGroup.Name = group.Name;
        dbGroup.UserId = userId;
        context.SaveChanges();

        return group;
    }

    public void DeleteGroup(Guid id, string userId)
    {
        try
        {
            var dbGroup = context.Groups.FirstOrDefault(x => x.Id == id && x.UserId == userId);

            if(dbGroup == null)
            {
                throw new KeyNotFoundException();
            }
            context.Groups.Remove(dbGroup);
            context.SaveChanges();
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
}
