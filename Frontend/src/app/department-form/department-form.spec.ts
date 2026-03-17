import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentForm } from './department-form';

describe('DepartmentForm', () => {
  let component: DepartmentForm;
  let fixture: ComponentFixture<DepartmentForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DepartmentForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DepartmentForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
