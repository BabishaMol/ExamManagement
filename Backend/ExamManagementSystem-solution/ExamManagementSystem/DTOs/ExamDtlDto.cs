using System.ComponentModel.DataAnnotations;

namespace ExamManagementSystem.DTOs
{
    public class ExamDtlDto
    {
        [Required]
        public int SubjectId { get; set; }

        [Range(0, 100, ErrorMessage = "Marks should be between 0 and 100.")]
        public int Marks { get; set; }
    }
}
