using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskCrud_20_10_23.DbContext;
using TaskCrud_20_10_23.Models;

namespace TaskCrud_20_10_23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepoJsonCrud _crud;
        public UsersController(IRepoJsonCrud crud)
        {
            _crud = crud;
        }
        [HttpGet("RetriveAll")]
        public List<Users> RetriveAll()
        {
            try
            {
                return _crud.GetAllUsers().ToList();
            }
            catch (Exception ex)
            {
                throw new (ex.Message);
            }
        }
        [HttpGet("{userId}")]
        public IActionResult GetUser(int userId)
        {
            try
            {
                var user = _crud.GetUserById(userId);
                if (user == null)
                {
                    return NotFound();
                }


                return Ok(user);
            }
            catch(Exception ex)
            {
                throw new (ex.Message);     
            }
        }
        [HttpPost]
        public IActionResult CreateUser([FromBody] Users user)
        {
            try {
                if (user == null)
                {
                    return BadRequest();
                }

                var createdUser = _crud.CreateUser(user);

                return CreatedAtAction(nameof(GetUser), new { userId = createdUser.UserId }, createdUser);
            }
            catch(Exception ex)
            {
                throw new(ex.Message);
            }
            }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] Users user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Invalid user data.");
                }

                var updatedUser = _crud.UpdateUser(id, user);

                if (updatedUser == null)
                {
                    return NotFound();
                }

                return Ok(updatedUser);
            }
            catch(Exception ex)
            {
                throw new(ex.Message);
            }
        }
        [HttpDelete]
        public IActionResult DeleteUser([FromQuery] int id)
        {
            try
            {
                bool success = _crud.DeleteUser(id);

                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch(Exception ex)
            {
                throw new (ex.Message);
            }
        }

    }
}
