using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using TrendencyDemo.CommonModule.Interfaces;

namespace TrendencyDemo.Web.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int GetUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User
                .FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public bool IsInRole(string role)
        {
            return _httpContextAccessor.HttpContext.User
                .FindAll(ClaimTypes.Role)
                .Any(x => x.Value == role);
        }
    }
}
