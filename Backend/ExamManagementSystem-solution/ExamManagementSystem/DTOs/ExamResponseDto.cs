namespace ExamManagementSystem.DTOs
{
    public class ExamResponseDto
    {
        public int MasterId { get; set; }

        public string StudentName { get; set; } = string.Empty;

        public int ExamYear { get; set; }

        public int TotalMark { get; set; }

        public string PassOrFail { get; set; } = string.Empty;

        public DateTime CreateTime { get; set; }
    }
}
