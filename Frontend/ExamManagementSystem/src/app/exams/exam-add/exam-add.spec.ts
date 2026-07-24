import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExamAdd } from './exam-add';

describe('ExamAdd', () => {
  let component: ExamAdd;
  let fixture: ComponentFixture<ExamAdd>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExamAdd]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExamAdd);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
