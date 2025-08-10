using TestConnectSupabase.Models;

namespace TestConnectSupabase.Dtos
{
    public class ClassUserDto
    {
        public int Id { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public List<User> ListUserInfo { get; set; } = new List<User>();
    }
}
