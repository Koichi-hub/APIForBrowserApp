using APIForBrowserApp.Enums;

namespace APIForBrowserApp.Entities
{
    public class User : BaseEntity
    {
        public int Id { get; set; }

        public RolesEnum Role {  get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
