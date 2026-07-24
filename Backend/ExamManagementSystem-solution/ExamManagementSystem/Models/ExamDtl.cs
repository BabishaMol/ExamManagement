using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamManagementSystem.Models;

public partial class ExamDtl
{
    public int DtlsId { get; set; }

    public int MasterId { get; set; }

    public int SubjectId { get; set; }

    [Range(0, 100)]
    public int Marks { get; set; }

    public virtual ExamMaster Master { get; set; } = null!;

    public virtual SubjectMst Subject { get; set; } = null!;
}
