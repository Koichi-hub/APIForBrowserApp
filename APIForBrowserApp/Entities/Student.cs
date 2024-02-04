namespace APIForBrowserApp.Entities
{
    public class Student : BaseEntity
    {
        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int Age { get; set; }

        public int GroupId { get; set; }

        public Group Group { get; set; } = null!;
    }
}
