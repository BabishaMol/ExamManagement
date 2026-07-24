import { Routes } from '@angular/router';
import { ExamAdd } from './exams/exam-add/exam-add';
import { ExamList } from './exams/exam-list/exam-list';
import { ExamEdit } from './exams/exam-edit/exam-edit';

export const routes: Routes = [

    //empty route
    {
        path: '',
        redirectTo: 'exam/list',
        pathMatch: 'full'
    },

    //lazy loading
    {
        path: 'exam/add',
        component: ExamAdd
    },

    {
        path: 'exam/list',
        component: ExamList
    },

    {
        path: 'exam/edit/:id',
        component: ExamEdit
    },

    //wildcard route
    {
        path: '**',
        redirectTo: 'exam/list'
    }

];