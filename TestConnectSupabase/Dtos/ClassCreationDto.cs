using Supabase.Postgrest.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TestConnectSupabase.Dtos
{
    public class ClassCreationDto
    { 
        [Required]
        public string ClassName { get; set; } = string.Empty;
    }
}
