namespace APIForBrowserApp.Models.Student
{
    public class CreateStudentRequest
    {
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int Age { get; set; }
    }
}
