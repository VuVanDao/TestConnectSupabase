using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using TestConnectSupabase.Dtos;
using TestConnectSupabase.Models;

namespace TestConnectSupabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly Supabase.Client _supabase;

        public ClassController(Supabase.Client supabase)
        {
            _supabase = supabase;
        }
        [HttpGet]
        public async Task<ActionResult<List<Class>>> GetUsers()
        {
            try
            {
                var ClassResult = await _supabase
                  .From<Class>()
                  .Get();
                var UserResult = await _supabase
                    .From<User>()
                    .Get();

                var classes = ClassResult.Models;
                var users = UserResult.Models;

                var ClassUserDto = new List<ClassUserDto>();
                foreach (var classItem in classes)
                {
                    var userInfo = users.Where(u => u.ClassId == classItem.Id).Select(u => u).ToList();
                    ClassUserDto.Add(new ClassUserDto
                    {
                        Id = classItem.Id,
                        ClassName = classItem.ClassName,
                        ListUserInfo = userInfo
                    });
                }
                return Ok(ClassUserDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClassCreationDto>> CreateClass([FromBody] Class classCreationDto)
        {
            try
            {
                var result = await _supabase
                    .From<Class>()
                    .Insert(classCreationDto);

                return Ok(result.Models.First());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Class>> UpdateUser(int id, [FromBody] Class classes)
        {
            try
            {
                classes.Id = id;
                var result = await _supabase
                    .From<Class>()
                    .Update(classes);

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
                    .From<Class>()
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
