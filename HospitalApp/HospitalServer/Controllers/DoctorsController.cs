using Microsoft.AspNetCore.Mvc;
using SecureRemotePassword;
using Eneter.SecureRemotePassword;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly MyDbContext _context;

    public DoctorsController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDoctors()
    {
        // Return minimal info for selection
        var doctors = await _context.Doctors
            .Include(d => d.User) // include basic user info
            .Select(d => new
            {
                d.DoctorID,
                d.Specialization,
                Name_First = d.User.Name_First,
                Name_Last = d.User.Name_Last,
                d.Office_Phone
            })
            .ToListAsync();

        return Ok(doctors);
    }
}
