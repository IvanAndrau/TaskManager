using TaskManagerAPI.EF;
using TaskManagerAPI.ViewModel;

namespace TaskManagerAPI.AppServices;

public class GroupService (TaskMgrContext context)
{
    public GroupModel AddGroup(GroupModel group)
    {
        var dbGroup = new Group();
        dbGroup.Name = group.Name;
        dbGroup.Id = group.Id;
        context.Groups.Add(dbGroup);
        context.SaveChanges();
        return group;
    }

    public IEnumerable<GroupModel> GetAllGroups()
    {
        List<GroupModel> groups = new List<GroupModel>();
        foreach (var group in context.Groups)
        {
            groups.Add(new GroupModel()
            {
                Id = group.Id,
                Name = group.Name
            });
        }
        return groups;
    }

    public GroupModel GetGroupById(Guid id)
    {
        var group = context.Groups.FirstOrDefault(x => x.Id == id);
        return new GroupModel()
        {
            Id = group.Id,
            Name = group.Name
        };
    }

    public GroupModel UpdateGroup(GroupModel group)
    {
        var dbGroup = context.Groups.FirstOrDefault(x => x.Id == group.Id);
        if(dbGroup == null)
        {
            throw new InvalidOperationException();
        }
        dbGroup.Name = group.Name;
        context.SaveChanges();

        return group;
    }

    public bool DeleteGroup(Guid id)
    {
        try
        {
            var dbGroup = context.Groups.FirstOrDefault(x => x.Id == id);
            context.Groups.Remove(dbGroup);
            context.SaveChanges();
        }
        catch
        {
            return false;
        }

        return true;
    }
}
