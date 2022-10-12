using System.Security.Claims;

namespace CodingWebsite.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpcontext;
        public UserService(IHttpContextAccessor httpcontext)
        {
            _httpcontext = httpcontext;
        }
        public string GetUserId()
        {
            return _httpcontext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public string GetUserName()
        {
            return _httpcontext.HttpContext.User?.FindFirstValue(ClaimTypes.Name);
        }
    }
}
