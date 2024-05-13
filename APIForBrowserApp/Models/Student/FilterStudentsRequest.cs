namespace APIForBrowserApp.Models.Student
{
    public class FilterStudentsRequest
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int Page { get; set; }

        public int PageCount { get; set; }
    }
}
