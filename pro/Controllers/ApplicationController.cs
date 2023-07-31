using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using pro.Models;
using pro.DTOs;
using pro.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace pro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly Context _context;
        private readonly UserManager<User> _userManager; // Add this line to include UserManager

        public ApplicantController(Context context, UserManager<User> userManager) // Add UserManager to the constructor
        {
            _context = context;
            _userManager = userManager; // Assign UserManager in the constructor
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Applicant>> CreateApplicantForUser(ApplicantDTO applicantDto)
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
            var newApplicant = new Applicant
            {
                UserId = userId,
                Title = applicantDto.Title,
                Dob = applicantDto.Dob,
                Gender = applicantDto.Gender,
                PhoneNo = applicantDto.PhoneNo,
                Email = applicantDto.Email,
                Address = applicantDto.Address,
                Street = applicantDto.Street,
                City = applicantDto.City,
                State = applicantDto.State,
                Zip = applicantDto.Zip,
                Country = applicantDto.Country,
            };

            // Associate the new Applicant with the User
            user.Applicant = newApplicant;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(newApplicant);
        }
    }
}
