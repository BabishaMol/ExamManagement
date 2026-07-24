import { CommonModule } from '@angular/common';
import { Component, OnInit, signal } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxPaginationModule } from 'ngx-pagination';
import { ExamResponse } from '../../models/exam-response';
import { ExamService } from '../../services/exam-service';
import { FilterExamPipe } from '../../pipes/filter-exam-pipe';

@Component({
  selector: 'app-exam-list',
  standalone: true,
  imports: [CommonModule,FormsModule,RouterModule,NgxPaginationModule,FilterExamPipe],
  templateUrl: './exam-list.html',
  styleUrl: './exam-list.scss'
})
export class ExamList implements OnInit {

  exams = signal<ExamResponse[]>([]);
  searchTerm: string = "";

  //pagination
  page: number = 1;
  itemsPerPage: number = 5;

  constructor(
    private examService: ExamService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {

    this.loadExams();

  }

  loadExams() {
    this.examService.getExamList().subscribe({
      next: (data) => {
        this.exams.set(data);
      },

      error: () => {
        this.toastr.error("Unable to load exam list");
      }
    });

  }

  editExam(id: number) {
    this.router.navigate(['/exam/edit', id]);
  }

}
