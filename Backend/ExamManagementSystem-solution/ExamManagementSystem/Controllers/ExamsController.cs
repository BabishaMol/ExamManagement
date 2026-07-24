
using ExamManagementSystem.DTOs;
using ExamManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService _service;

        public ExamsController(IExamService service)
        {
            _service = service;
        }

        // Get All Students
        [HttpGet("GetStudents")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _service.GetStudentsAsync();
            return Ok(students);
        }

        // Get All Subjects
        [HttpGet("GetSubjects")]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await _service.GetSubjectsAsync();
            return Ok(subjects);
        }

        // Add Exam
        [HttpPost("AddExam")]
        public async Task<IActionResult> AddExam([FromBody] ExamMasterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.AddExamAsync(dto);

            return Ok(new
            {
                Message = "Exam Saved Successfully",
                Data = result
            });
        }

        // Get Exam List
        [HttpGet("GetExamList")]
        public async Task<IActionResult> GetExamList()
        {
            var exams = await _service.GetExamListAsync();
            return Ok(exams);
        }

        // Get Exam By Id
        [HttpGet("GetExamById/{id}")]
        public async Task<IActionResult> GetExamById(int id)
        {
            var exam = await _service.GetExamByIdAsync(id);

            if (exam == null)
                return NotFound("Exam not found.");

            return Ok(exam);
        }

        // Update Exam
        [HttpPut("UpdateExam")]
        public async Task<IActionResult> UpdateExam([FromBody] ExamMasterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.UpdateExamAsync(dto);

            return Ok(new
            {
                Message = "Exam Updated Successfully",
                Data = result
            });
        }

        // Delete Subject
        [HttpDelete("DeleteSubject/{dtlsId}")]
        public async Task<IActionResult> DeleteSubject(int dtlsId)
        {
            await _service.DeleteSubjectAsync(dtlsId);

            return Ok(new
            {
                Message = "Subject Deleted Successfully"
            });
        }
    }
}
