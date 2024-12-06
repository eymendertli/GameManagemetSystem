using System.ComponentModel.DataAnnotations;

namespace BLL.DAL;

public class GameAccount
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(40)]
    public string GameName { get; set; }
    
    public int Level { get; set; }
    
    public int PlayerId { get; set; }
    
    public Player Player { get; set; }
}