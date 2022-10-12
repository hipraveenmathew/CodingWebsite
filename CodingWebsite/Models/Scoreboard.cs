namespace CodingWebsite.Models
{
    public class Scoreboard
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public float? Score { get; set; }
        public int? Semester { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
