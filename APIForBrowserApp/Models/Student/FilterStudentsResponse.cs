namespace APIForBrowserApp.Models.Student
{
    public class FilterStudentsResponse
    {
        public List<GetStudentResponse> Students { get; set; } = new List<GetStudentResponse>();

        public int PageTotal { get; set; }

        public int Page { get; set; }

        public int PageCount { get; set; }
    }
}
