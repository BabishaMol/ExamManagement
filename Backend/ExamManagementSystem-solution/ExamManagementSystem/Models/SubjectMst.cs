using System;
using System.Collections.Generic;

namespace ExamManagementSystem.Models;

public partial class SubjectMst
{
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public virtual ICollection<ExamDtl> ExamDtls { get; set; } = new List<ExamDtl>();
}
