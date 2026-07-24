

using ExamManagementSystem.DTOs;
using ExamManagementSystem.Models;
using ExamManagementSystem.Repositories;

namespace ExamManagementSystem.Services
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _repository;

        public ExamService(IExamRepository repository)
        {
            _repository = repository;
        }

        // Get Students
        public async Task<IEnumerable<StudentMst>> GetStudentsAsync()
        {
            return await _repository.GetStudentsAsync();
        }

        // Get Subjects
        public async Task<IEnumerable<SubjectMst>> GetSubjectsAsync()
        {
            return await _repository.GetSubjectsAsync();
        }

        // Add Exam
        public async Task<ExamResponseDto> AddExamAsync(ExamMasterDto dto)
        {
            var exam = new ExamMaster
            {
                StudentId = dto.StudentId,
                ExamYear = dto.ExamYear,
                ExamDtls = dto.ExamDtls.Select(x => new ExamDtl
                {
                    SubjectId = x.SubjectId,
                    Marks = x.Marks
                }).ToList()
            };

            var result = await _repository.AddExamAsync(exam);

            return new ExamResponseDto
            {
                MasterId = result.MasterId,
                StudentName = result.Student?.StudentName ?? "",
                ExamYear = result.ExamYear,
                TotalMark = result.TotalMark,
                PassOrFail = result.PassOrFail,
                CreateTime = result.CreateTime
            };
        }

        // Get Exam List
        public async Task<IEnumerable<ExamResponseDto>> GetExamListAsync()
        {
            var exams = await _repository.GetExamListAsync();

            return exams.Select(x => new ExamResponseDto
            {
                MasterId = x.MasterId,
                StudentName = x.Student.StudentName,
                ExamYear = x.ExamYear,
                TotalMark = x.TotalMark,
                PassOrFail = x.PassOrFail,
                CreateTime = x.CreateTime
            });
        }

        // Get Exam By Id
        public async Task<ExamMasterDto?> GetExamByIdAsync(int id)
        {
            var exam = await _repository.GetExamByIdAsync(id);

            if (exam == null)
                return null;

            return new ExamMasterDto
            {
                MasterId = exam.MasterId,
                StudentId = exam.StudentId,
                ExamYear = exam.ExamYear,

                ExamDtls = exam.ExamDtls.Select(x => new ExamDtlDto
                {
                    SubjectId = x.SubjectId,
                    Marks = x.Marks
                }).ToList()
            };
        }

        // Update Exam
        public async Task<ExamResponseDto> UpdateExamAsync(ExamMasterDto dto)
        {
            var exam = new ExamMaster
            {
                MasterId = dto.MasterId,
                StudentId = dto.StudentId,
                ExamYear = dto.ExamYear,
                CreateTime = DateTime.Now,


                ExamDtls = dto.ExamDtls.Select(x => new ExamDtl
                {
                    SubjectId = x.SubjectId,
                    Marks = x.Marks
                }).ToList()
            };

            var result = await _repository.UpdateExamAsync(exam);

            return new ExamResponseDto
            {
                MasterId = result.MasterId,
                StudentName = result.Student?.StudentName ?? "",
                ExamYear = result.ExamYear,
                TotalMark = result.TotalMark,
                PassOrFail = result.PassOrFail,
                CreateTime = result.CreateTime 
            };
        }

        // Delete Subject
        public async Task DeleteSubjectAsync(int dtlsId)
        {
            await _repository.DeleteSubjectAsync(dtlsId);
        }
    }
}
