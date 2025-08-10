using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using TestConnectSupabase.Dtos;
using TestConnectSupabase.Models;

namespace TestConnectSupabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Supabase.Client _supabase;

        public UserController(Supabase.Client supabase)
        {
            _supabase = supabase;
        }
        [HttpGet]
        public async Task<ActionResult<List<UserClassDto>>> GetUsers()
        {
            try
            {
                
                var UserResult = await _supabase
                    .From<User>()
                    .Get();
                var classResult = await _supabase
                .From<Class>()
                .Get();
                var classIds = UserResult.Models.Select(p => p.ClassId).Distinct().ToList();
                var ClassUserDto = new List<UserClassDto>();
                foreach (var user in UserResult.Models)
                {
                 var res = classIds.Find(id => id == user.ClassId);
                    if (res != null)
                    {
                        ClassUserDto.Add(new UserClassDto
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Email = user.Email,
                            ClassId = user.ClassId,
                            ClassInfo = classResult.Models.Find(x => x.Id == user.ClassId)
                        });
                    }
                }
                return Ok(ClassUserDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            Console.WriteLine(user.Name);
            try
            {
                var ClassResult = await _supabase
                    .From<Class>()
                    .Where(x => x.Id == user.ClassId)
                    .Get();
                if(!ClassResult.Models.Any())
                {
                    return BadRequest(new { error = "Class not found" });
                }
                user.ClassId = ClassResult.Models.First().Id;
                var result = await _supabase
                    .From<User>()
                    .Insert(user);

                return Ok(result.Models.First());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User user)
        {
            Console.WriteLine(user.Id);
            Console.WriteLine(user.Name);
            Console.WriteLine(user.Email);
            try
            {
                user.Id = id;
                var result = await _supabase
                    .From<User>()
                    .Update(user);

                return Ok(result.Models.First());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _supabase
                    .From<User>()
                    .Where(x => x.Id == id)
                    .Delete();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
