

using ExamManagementSystem.DTOs;
using ExamManagementSystem.Models;

namespace ExamManagementSystem.Repositories
{
    public interface IExamRepository
    {
        Task<ExamMaster> AddExamAsync(ExamMaster exam);

        Task<IEnumerable<ExamMaster>> GetExamListAsync();

        Task<ExamMaster?> GetExamByIdAsync(int id);

        Task<ExamMaster> UpdateExamAsync(ExamMaster exam);

        Task DeleteSubjectAsync(int dtlsId);

        Task<IEnumerable<StudentMst>> GetStudentsAsync();

        Task<IEnumerable<SubjectMst>> GetSubjectsAsync();
    }
}
