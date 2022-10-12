namespace CodingWebsite.Models
{
    public class Questions
    {
        public int Id { get; set; }
        public string TeacherId { get; set; }
        public string QuestionHeading { get; set; }
        public string TheQuestion { get; set; }
        public string? Input1 { get; set; }
        public string? Output1 { get; set; }
        public string? Input2 { get; set; }
        public string? Output2 { get; set; }
        public string? Input3 { get; set; }
        public string? Output3 { get; set; }
        public string? TheAnswer { get; set; }
        public DateTime? FinalDate { get; set; }
        public float? Score { get; set; }
        public int? Difficulty { get; set; }

        public int? StartedCount { get; set; }

        public int? ProcessingCount { get; set; }

        public int? CompletedCount { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
       
    }
}
