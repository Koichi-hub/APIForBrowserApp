namespace APIForBrowserApp.Entities
{
    public class Group : BaseEntity
    {
        public int Id { get; set; }

        public int Grade { get; set; }

        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; } = null!;
    }
}
