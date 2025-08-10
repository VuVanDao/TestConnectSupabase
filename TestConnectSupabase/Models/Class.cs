using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.ComponentModel.DataAnnotations;

namespace TestConnectSupabase.Models
{
    [Table("Class")]
    public class Class:BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }
        [Column("className")]
        [Required]
        public string? ClassName { get; set; }
        [Column("created_at")]
        public DateTime Created_at { get; set; } = DateTime.UtcNow;
        //1-n
        //public List<User>? ListUsersInfo{ get; set; } = new List<User>();
    }
    
}
