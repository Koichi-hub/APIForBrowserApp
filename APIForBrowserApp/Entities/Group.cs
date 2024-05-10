namespace APIForBrowserApp.Entities
{
    public class Group : BaseEntity
    {
        public int Id { get; set; }

        public int Grade { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();

        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
