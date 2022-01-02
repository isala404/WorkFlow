using System;
using System.IO;
using WorkFlow.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkFlow.Shared.Entities;
using WorkFlow.Shared.Interfaces;


namespace WorkFlow.Server.Models
{
    public class UtilityService: IUtility
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UtilityService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User?> GetUser()
        {
            if(_httpContextAccessor.HttpContext == null) return null;
            String? userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }
    }
}
