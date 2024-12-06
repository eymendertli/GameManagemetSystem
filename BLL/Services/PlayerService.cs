using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public interface IPlayerService
{
    public IQueryable<PlayerModel> Query();
    public ServiceBase Create(Player record);
    public ServiceBase Update(Player record);
    public ServiceBase Delete(int id);

}
public class PlayerService : ServiceBase , IPlayerService
{
    public PlayerService(Db db) : base(db)
    {
        
    }

    public IQueryable<PlayerModel> Query()
    {
        return _db.Players.OrderBy(p => p.UserName).Select(p => new PlayerModel {Record = p});
    }

    public ServiceBase Create(Player record)
    {
        if(_db.Players.Any(p => p.UserName.ToLower() == record.UserName.ToLower().Trim()))
            return Error("Username is taken!");
        
        record.UserName = record.UserName.Trim();
        _db.Players.Add(record);
        _db.SaveChanges();
        
        return Success("Created successfully!");
    }

    public ServiceBase Update(Player record)
    {
        if(_db.Players.Any(p => p.Id != record.Id && p.UserName.ToLower() == record.UserName.ToLower().Trim()))
            return Error("Username is already exists!");
        
        var entity = _db.Players.SingleOrDefault(p => p.Id == record.Id);
        if (entity == null)
            return Error("Player not found!");
        entity.UserName = record.UserName.Trim();
        _db.Players.Update(entity);
        _db.SaveChanges();
        
        return Success("Updated successfully!");
    }

    public ServiceBase Delete(int id)
    {
        var entity = _db.Players.Include(p => p.GameAccounts).SingleOrDefault(p => p.Id == id);
        if (entity == null)
            return Error("Player not found!");
        if(entity.GameAccounts.Any())
            return Error("Player is associated with a game account!");
        _db.Players.Remove(entity);
        _db.SaveChanges();
        
        return Success("Deleted successfully!");
    }
}