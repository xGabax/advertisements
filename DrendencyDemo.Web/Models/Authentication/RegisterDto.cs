
using TrendencyDemo.Dal.Entities;

namespace DrendencyDemo.Web.Models.Authentication
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User ToEntity()
        {
            return new User
            {
                UserName = UserName,
                Email = Email
            };
        }
    }
}
