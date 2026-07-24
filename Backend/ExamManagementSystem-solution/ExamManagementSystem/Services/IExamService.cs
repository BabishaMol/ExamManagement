
using ExamManagementSystem.DTOs;
using ExamManagementSystem.Models;

namespace ExamManagementSystem.Services
{
    public interface IExamService
    {
        Task<IEnumerable<StudentMst>> GetStudentsAsync();

        Task<IEnumerable<SubjectMst>> GetSubjectsAsync();

        Task<ExamResponseDto> AddExamAsync(ExamMasterDto dto);

        Task<IEnumerable<ExamResponseDto>> GetExamListAsync();

        Task<ExamMasterDto?> GetExamByIdAsync(int id);

        Task<ExamResponseDto> UpdateExamAsync(ExamMasterDto dto);

        Task DeleteSubjectAsync(int dtlsId);
    }
}
