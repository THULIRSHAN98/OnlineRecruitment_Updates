//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Identity;
//using pro.Models;
//using pro.Data;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using System.Security.Claims;
//using pro.DTOs.Inside;
//using System.Collections.Generic;

//namespace pro.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserCompanyController : ControllerBase
//    {
//        private readonly Context _context;
//        private readonly UserManager<User> _userManager;

//        public UserCompanyController(Context context, UserManager<User> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }

//        [Authorize]
//        [HttpPost("assign-company")]
//        public async Task<ActionResult<User_Company>> AssignCompanyForUser(UserCompanyDTO userCompanyDto)
//        {
//            try
//            {
//                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//                var user = await _userManager.FindByIdAsync(userId);

//                if (user == null)
//                {
//                    return NotFound("User not found.");
//                }

//                var userCompanyAssignment = new User_Company
//                {
//                    UserId = userId,
//                    id = userCompanyDto.id,
//                };

//                // Associate the new User_Company with the User
//                user.User_Company = new List<User_Company> { userCompanyAssignment };

//                // Save changes to the database
//                await _context.SaveChangesAsync();

//                return Ok(userCompanyAssignment);
//            }
//            catch (Exception ex)
//            {
//                // Log the exception if needed

//                // Set HTTP status code to 500 (Internal Server Error)
//                return StatusCode(500, "Error assigning company.");
//            }
//        }

//        // ... Other actions can be added here
//    }
//}
