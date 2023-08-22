using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pro.Data;
using pro.DTOs.Account;
using pro.DTOs.Inside;
using pro.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace pro.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentUserController : ControllerBase
    {
        private readonly Context _context;
        private readonly UserManager<User> _userManager;

        public DepartmentUserController(Context context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost("AddUserToDepartment")]
        public async Task<ActionResult> AddUserToDepartment(DepartmentUserDTO departmentUserDTO)
        {
            // Get the authenticated user's UserId from the ClaimsPrincipal
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Check if the user exists in the database
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Find the department by name
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentName == departmentUserDTO.DepartmentName);
            if (department == null)
            {
                return NotFound("Department not found.");
            }

            // Add the user to the DepartmentUser table
            var departmentUser = new DepartmentUser
            {
                UserId = user.Id,
                DepartmentID = department.DepartmentID
            };

            _context.DepartmentUsers.Add(departmentUser);
            await _context.SaveChangesAsync();

            return Ok("User added to department successfully.");
        }

       
    }
}
