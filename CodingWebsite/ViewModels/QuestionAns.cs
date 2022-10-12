namespace CodingWebsite.ViewModels
{
    public class QuestionAns
    {
        public string TheQuestion { get; set; }
        public string? SAnswer { get; set; }
        public int Id { get; set; }
        public string? language { get; set; }
        public string? versionIndex { get; set; }

        public int QuesId { get; set; }
        public string StudId { get; set; }
        public string error { get; set; }
        public int Success { get; set; }
        public int StestCase { get; set; }
        public int Ttestcase { get; set; }


        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
