using TestConnectSupabase.Models;

namespace TestConnectSupabase.Dtos
{
    public class UserClassDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int? ClassId { get; set; }
        public Class ClassInfo { get; set; } 
    }
}
