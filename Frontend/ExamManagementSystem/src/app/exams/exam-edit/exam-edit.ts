import { CommonModule } from '@angular/common';
import { Component, OnInit, signal } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { Student } from '../../models/student';
import { Subject } from '../../models/subject';
import { ExamDetail } from '../../models/exam-detail';
import { ExamMaster } from '../../models/exam-master';
import { ExamService } from '../../services/exam-service';

@Component({
  selector: 'app-exam-edit',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './exam-edit.html',
  styleUrl: './exam-edit.scss'
})
export class ExamEdit implements OnInit {

  students = signal<Student[]>([]);
  subjects = signal<Subject[]>([]);
  totalMark = signal<number>(0);
  passOrFail = signal<string>('FAIL');
  errorMessage = signal('');
  currentYear = new Date().getFullYear();




  masterId!: number;

  examForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private examService: ExamService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {

    this.createForm();

    this.loadStudents();

    this.loadSubjects();

    this.masterId = Number(this.route.snapshot.paramMap.get('id'));

    this.loadExam();

  }

  createForm() {

    this.examForm = this.fb.group({

      masterId: [0],

      studentId: ['', Validators.required],

      examYear: [null, [Validators.required, Validators.max(this.currentYear)]],


      examDtls: this.fb.array([])

    });

  }

  get examDtls(): FormArray {

    return this.examForm.get('examDtls') as FormArray;

  }

  loadStudents() {

    this.examService.getStudents().subscribe({

      next: res => {

        this.students.set(res);

      }

    });

  }

  loadSubjects() {

    this.examService.getSubjects().subscribe({

      next: res => {

        this.subjects.set(res);

      }

    });

  }

  availableSubjects(index: number): Subject[] {

    // Get all selected subject ids except the current row
    const selectedIds = this.examDtls.controls
      .map((control, i) => i !== index ? Number(control.get('subjectId')?.value) : null)
      .filter(id => id !== null && id !== 0);

    // Keep the current row's selected subject
    const currentSubjectId = Number(
      this.examDtls.at(index).get('subjectId')?.value
    );

    return this.subjects().filter(subject =>
      subject.subjectId === currentSubjectId ||
      !selectedIds.includes(subject.subjectId)
    );

  }

  loadExam() {

    this.examService.getExamById(this.masterId).subscribe({

      next: (exam: ExamMaster) => {

        this.examForm.patchValue({

          masterId: exam.masterId,

          studentId: exam.studentId,

          examYear: exam.examYear

        });

        this.examDtls.clear();

        exam.examDtls.forEach((x: ExamDetail) => {

          this.examDtls.push(

            this.fb.group({

              dtlsId: [x.dtlsId],

              subjectId: [x.subjectId, Validators.required],

              marks: [
                x.marks,
                [
                  Validators.required,
                  Validators.min(0),
                  Validators.max(100)
                ]
              ]

            })

          );

        });

        this.calculateTotal();

      }

    });

  }

  addSubject() {

    this.examDtls.push(

      this.fb.group({

        dtlsId: [0],

        subjectId: ['', Validators.required],

        marks: [
          0,
          [
            Validators.required,
            Validators.min(0),
            Validators.max(100)
          ]
        ]

      })

    );

  }

  removeSubject(index: number) {

    this.examDtls.removeAt(index);

    this.calculateTotal();

  }

  

  calculateTotal() {

    let total = 0;

    let pass = true;

    this.examDtls.controls.forEach(control => {

      const mark = Number(control.get('marks')?.value);

      total += mark;

      if (mark < 25) {
        pass = false;
      }

    });

    this.totalMark.set(total);

    this.passOrFail.set(pass ? 'PASS' : 'FAIL');

  }

  updateExam() {

    if (this.examForm.invalid) {

      this.examForm.markAllAsTouched();

      return;

    }

    this.examService.updateExam(this.examForm.value).subscribe({

      next: () => {

        this.toastr.success("Exam Updated Successfully");

        this.router.navigate(['/exam/list']);

      },

      error: () => {

        this.toastr.error("Update Failed");

      }

    });

  }

}
