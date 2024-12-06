using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public interface IGameAccountService
{
    public IQueryable<GameAccountModel> Query();
    public ServiceBase Create(GameAccount record);
    public ServiceBase Update(GameAccount record);
    public ServiceBase Delete(int id);
}

public class GameAccountService : ServiceBase, IGameAccountService
{
    public GameAccountService(Db db) : base(db)
    {
    }

    public IQueryable<GameAccountModel> Query()
    {
        return _db.GameAccounts.Include(g => g.Player).OrderBy(g => g.GameName) .Select(g => new GameAccountModel() { Record = g });
    }

    public ServiceBase Create(GameAccount record)
    {
        if (_db.GameAccounts.Any(g => g.GameName.ToLower() == record.GameName.ToLower().Trim() && g.PlayerId == record.PlayerId))
            return Error("This game account already exists for the player!");
        
        record.GameName = record.GameName.Trim();
        _db.GameAccounts.Add(record);
        _db.SaveChanges();

        return Success("Game account created successfully!");
    }


    public ServiceBase Update(GameAccount record)
    {
        if (_db.GameAccounts.Any(g => g.Id != record.Id && g.GameName.ToLower() == record.GameName.ToLower().Trim() && g.PlayerId == record.PlayerId))
            return Error("This game account already exists for the player!");
        
        record.GameName = record.GameName?.Trim();
        _db.GameAccounts.Update(record);
        _db.SaveChanges();

        return Success("Game account created successfully!");
    }

    public ServiceBase Delete(int id)
    {
        var entity = _db.GameAccounts.SingleOrDefault(g => g.Id == id);
        if(entity == null)
            return Error("This game account does not exist!");
        _db.GameAccounts.Remove(entity);
        _db.SaveChanges();
        return Success("Game account deleted successfully!");
    }
}