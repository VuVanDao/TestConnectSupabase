using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Text.Json.Serialization;

namespace TestConnectSupabase.Models
{
    [Table("User")]
    public class User:BaseModel
    {
        [PrimaryKey("id",false)]
        [JsonIgnore]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        [Column("created_at")]
        public DateTime Created_at { get; set; }
        //1-1
        [Column("class_id")]
        [JsonIgnore]
        public int? ClassId { get; set; }

    }
}
