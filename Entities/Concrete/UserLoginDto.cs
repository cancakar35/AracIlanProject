using Entities.Abstract;

namespace Entities.Concrete
{
    public class UserLoginDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
