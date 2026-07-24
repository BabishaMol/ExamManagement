import { Pipe, PipeTransform } from '@angular/core';
import { ExamResponse } from '../models/exam-response';

@Pipe({
  name: 'filterExam',
  standalone: true
})
export class FilterExamPipe implements PipeTransform {

  transform(exams: ExamResponse[], searchTerm: string): ExamResponse[] {

    if (!searchTerm)
      return exams;

    searchTerm = searchTerm.toLowerCase();

    return exams.filter(x =>

      x.studentName.toLowerCase().includes(searchTerm) ||

      x.examYear.toString().includes(searchTerm)

    );

  }

}
