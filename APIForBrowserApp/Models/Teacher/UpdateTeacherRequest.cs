namespace APIForBrowserApp.Models.Teacher
{
    public class UpdateTeacherRequest
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int Age { get; set; }
    }
}
