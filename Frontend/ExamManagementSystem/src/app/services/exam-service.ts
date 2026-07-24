import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Student } from '../models/student';
import { Subject } from '../models/subject';
import { ExamMaster } from '../models/exam-master';
import { ExamResponse } from '../models/exam-response';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ExamService {

  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  // ==========================
  // Student
  // ==========================

  getStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(`${this.apiUrl}/GetStudents`);
  }

  // ==========================
  // Subject
  // ==========================

  getSubjects(): Observable<Subject[]> {
    return this.http.get<Subject[]>(`${this.apiUrl}/GetSubjects`);
  }

  // ==========================
  // Add Exam
  // ==========================

  addExam(exam: ExamMaster): Observable<any> {
    return this.http.post(this.apiUrl + "/AddExam", exam);
  }

  // ==========================
  // Get Exam List
  // ==========================

  getExamList(): Observable<ExamResponse[]> {
    return this.http.get<ExamResponse[]>(this.apiUrl+"/GetExamList");
  }

  // ==========================
  // Get Exam By Id
  // ==========================

  getExamById(id: number): Observable<ExamMaster> {
    return this.http.get<ExamMaster>(`${this.apiUrl+"/GetExamById"}/${id}`);
  }

  // ==========================
  // Update Exam
  // ==========================

  updateExam(exam: ExamMaster): Observable<any> {
    return this.http.put(this.apiUrl+"/UpdateExam", exam);
  }

  // ==========================
  // Delete Subject
  // ==========================

  deleteSubject(dtlsId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/DeleteSubject/${dtlsId}`);
  }

}
