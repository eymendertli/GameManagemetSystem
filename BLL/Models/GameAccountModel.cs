using BLL.DAL;

namespace BLL.Models;

public class GameAccountModel
{
    public GameAccount Record { get; set; }

    public string GameName => Record.GameName;
    
    public int Level => Record.Level;

    public string Player => Record.Player?.UserName;
}