using System.ComponentModel.DataAnnotations;

namespace ExamManagementSystem.DTOs
{
    public class ExamMasterDto
    {
        public int MasterId { get; set; }   

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int ExamYear { get; set; }

        public List<ExamDtlDto> ExamDtls { get; set; } = new();
    }
}
