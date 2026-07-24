
using ExamManagementSystem.DTOs;
using ExamManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamManagementSystem.Repositories
{
    public class ExamRepository : IExamRepository
    {
        private readonly ExamManagementSystemContext _context;

        public ExamRepository(ExamManagementSystemContext context)
        {
            _context = context;
        }

        // Get Students
        public async Task<IEnumerable<StudentMst>> GetStudentsAsync()
        {
            return await _context.StudentMsts.ToListAsync();
        }

        // Get Subjects
        public async Task<IEnumerable<SubjectMst>> GetSubjectsAsync()
        {
            return await _context.SubjectMsts.ToListAsync();
        }

        // Add Exam
        public async Task<ExamMaster> AddExamAsync(ExamMaster exam)
        {
            exam.TotalMark = exam.ExamDtls.Sum(x => x.Marks);

            exam.PassOrFail = exam.ExamDtls.All(x => x.Marks >= 25)
                ? "PASS"
                : "FAIL";

            exam.CreateTime = DateTime.Now;

            await _context.ExamMasters.AddAsync(exam);
            await _context.SaveChangesAsync();

            return exam;
        }

        // Get All Exams
        public async Task<IEnumerable<ExamMaster>> GetExamListAsync()
        {
            return await _context.ExamMasters
                .Include(x => x.Student)
                .Include(x => x.ExamDtls)
                .ThenInclude(x => x.Subject)
                .ToListAsync();
        }

        // Get Exam By Id
        public async Task<ExamMaster?> GetExamByIdAsync(int id)
        {
            return await _context.ExamMasters
                .Include(x => x.Student)
                .Include(x => x.ExamDtls)
                .ThenInclude(x => x.Subject)
                .FirstOrDefaultAsync(x => x.MasterId == id);
        }

        // Update Exam
        public async Task<ExamMaster> UpdateExamAsync(ExamMaster exam)
        {
            // Get existing exam from database
            var existingExam = await _context.ExamMasters
                .Include(x => x.ExamDtls)
                .FirstOrDefaultAsync(x => x.MasterId == exam.MasterId);

            if (existingExam == null)
                throw new Exception("Exam not found");

            // Update Master fields
            existingExam.StudentId = exam.StudentId;
            existingExam.ExamYear = exam.ExamYear;

            // Keep original CreateTime
            existingExam.CreateTime = existingExam.CreateTime;

            // Delete old subject rows
            _context.ExamDtls.RemoveRange(existingExam.ExamDtls);

            // Add new subject rows
            existingExam.ExamDtls = exam.ExamDtls.Select(x => new ExamDtl
            {
                SubjectId = x.SubjectId,
                Marks = x.Marks
            }).ToList();

            // Calculate Total
            existingExam.TotalMark = existingExam.ExamDtls.Sum(x => x.Marks);

            // Calculate Result
            existingExam.PassOrFail = existingExam.ExamDtls.All(x => x.Marks >= 25)
                ? "PASS"
                : "FAIL";

            await _context.SaveChangesAsync();

            return existingExam;
        }

        // Delete Subject
        public async Task DeleteSubjectAsync(int dtlsId)
        {
            var data = await _context.ExamDtls
                .FirstOrDefaultAsync(x => x.DtlsId == dtlsId);

            if (data != null)
            {
                _context.ExamDtls.Remove(data);
                await _context.SaveChangesAsync();
            }
        }
    }
}
