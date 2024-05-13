using APIForBrowserApp.Enums;

namespace APIForBrowserApp.Models
{
    public class LoginResponse
    {
        public int Id { get; set; }

        public RolesEnum Role { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
