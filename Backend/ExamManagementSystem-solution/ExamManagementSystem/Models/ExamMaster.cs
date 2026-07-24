using System;
using System.Collections.Generic;

namespace ExamManagementSystem.Models;

public partial class ExamMaster
{
    public int MasterId { get; set; }

    public int StudentId { get; set; }

    public int ExamYear { get; set; }

    public int TotalMark { get; set; }

    public string PassOrFail { get; set; } = null!;

    public DateTime CreateTime { get; set; }

    public virtual ICollection<ExamDtl> ExamDtls { get; set; } = new List<ExamDtl>();

    public virtual StudentMst Student { get; set; } = null!;
}
