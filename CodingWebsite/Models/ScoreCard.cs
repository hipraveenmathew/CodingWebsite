namespace CodingWebsite.Models
{
    public class ScoreCard
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string QuesHead { get; set; }
        public int QuestionId { get; set; }

        public float? Score { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
