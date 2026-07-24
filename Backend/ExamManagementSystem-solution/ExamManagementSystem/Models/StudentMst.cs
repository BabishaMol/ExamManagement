using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamManagementSystem.Models;

public partial class StudentMst
{
    public int StudentId { get; set; }

    [Required]
    [StringLength(250, MinimumLength = 5)]
    public string StudentName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Mail { get; set; } = null!;

    public virtual ICollection<ExamMaster> ExamMasters { get; set; } = new List<ExamMaster>();
}
