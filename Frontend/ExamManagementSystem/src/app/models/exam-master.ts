import { ExamDetail } from "./exam-detail";

export class ExamMaster {

  masterId: number = 0;

  studentId: number = 0;

  studentName: string = '';

  mail: string = '';

  examYear: number = 0;

  totalMark: number = 0;

  passOrFail: string = '';

  createTime?: Date;

  examDtls: ExamDetail[] = [];
}
