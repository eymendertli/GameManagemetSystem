using BLL.DAL;

namespace BLL.Services.Bases;

public abstract class ServiceBase
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;

    protected readonly Db _db;
    
    protected ServiceBase(Db db)
    {
        _db = db;
    }

    public ServiceBase Success(string message = "")
    {
        IsSuccess = true;
        Message = message;
        return this;
    }

    public ServiceBase Error(string message = "")
    {
        IsSuccess = false;
        Message = message;
        return this;
    }
    
}