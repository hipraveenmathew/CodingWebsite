namespace CodingWebsite.Models
{
    public class Answers
    {
        public int Id { get; set; }

        public int QuesId { get; set; }
        public string StudId { get; set; }

        public string? SAnswer { get; set; }
        public string? language { get; set; }
        public string? versionIndex { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
