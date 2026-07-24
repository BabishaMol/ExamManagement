import { CommonModule } from '@angular/common';
import { Component, OnInit, signal } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { Student } from '../../models/student';
import { Subject } from '../../models/subject';
import { ExamService } from '../../services/exam-service';


@Component({
  selector: 'app-exam-add',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './exam-add.html',
  styleUrl: './exam-add.scss'
})
export class ExamAdd implements OnInit {

  students = signal<Student[]>([]);
  subjects = signal<Subject[]>([]);
  totalMark = signal<number>(0);
  passOrFail = signal<string>('FAIL');
  errorMessage = signal('');
  currentYear = new Date().getFullYear();


  examForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private examService: ExamService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {

    this.createForm();

    this.loadStudents();

    this.loadSubjects();

    this.addSubject();
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
      next: (res) => {
        this.students.set(res);
      }
    });

  }

  loadSubjects() {

    this.examService.getSubjects().subscribe({
      next: (res) => {
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

  addSubject() {

    const group = this.fb.group({

      dtlsId: [0],

      subjectId: ['', Validators.required],

      marks: [0, [
        Validators.required,
        Validators.min(0),
        Validators.max(100)
      ]]

    });

    this.examDtls.push(group);

    this.calculateTotal();

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

  saveExam() {

    if (this.examForm.invalid) {

      this.examForm.markAllAsTouched();

      return;

    }

    this.examService.addExam(this.examForm.value).subscribe({

      next: () => {

        this.toastr.success("Exam Saved Successfully");

        this.examForm.reset();

        this.examDtls.clear();

        this.addSubject();

        this.totalMark.set(0);

        this.router.navigate(['/exam/list']);

      },

      error: err => {

        this.toastr.error("Failed to Save.. This student already Saved with this Examyear");

      }

    });

  }

}
