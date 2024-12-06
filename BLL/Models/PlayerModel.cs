using BLL.DAL;

namespace BLL.Models;

public class PlayerModel
{
    public Player Record { get; set; }

    public string UserName => Record.UserName;
    
    public string Email => Record.Email;
    
}