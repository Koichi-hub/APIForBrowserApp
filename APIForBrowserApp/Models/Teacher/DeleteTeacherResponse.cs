namespace APIForBrowserApp.Models.Teacher
{
    public class DeleteTeacherResponse
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DeletedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
