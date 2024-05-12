namespace APIForBrowserApp.Entities
{
    public class Student : BaseEntity
    {
        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DeletedAt { get; set; }
    }
}
