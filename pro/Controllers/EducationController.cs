
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pro.Data;
using pro.DTOs.Inside;
using pro.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace pro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly Context _context;
        private readonly UserManager<User> _userManager; // Add this line to include UserManager

        public EducationController(Context context, UserManager<User> userManager) // Add UserManager to the constructor
        {
            _context = context;
            _userManager = userManager; // Assign UserManager in the constructor
        }

        [Authorize]
        [HttpPost("app")]
        public async Task<ActionResult<Education>> CreateApplicantForUser(EducationDTO educationDTO)
        {
            // Get the authenticated user's UserId from the ClaimsPrincipal
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Check if the user exists in the database
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Create a new Applicant
            var newEducation = new Education
            {
                UserId = userId,
                CurrentStatus=educationDTO.CurrentStatus,
                Qulification= educationDTO.Qulification,
                InsituteName= educationDTO.InsituteName,
                Yearattained=educationDTO.Yearattained,
                FieldOfStudy=educationDTO.FieldOfStudy,
                SoftSkills=educationDTO.SoftSkills,
                HardSkills=educationDTO.HardSkills,
                Languages =educationDTO.Languages,

            };

            // Associate the new Applicant with the User
            user.Educations = new List<Education> { newEducation };


            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(newEducation);
        }
    }
}
