using System.ComponentModel.DataAnnotations;

namespace BLL.DAL;

public class Player
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(30)]
    public string UserName { get; set; }
    
    [Required]
    [StringLength(30)]
    public string Email { get; set; }
    
    public List<GameAccount> GameAccounts { get; set; }
}