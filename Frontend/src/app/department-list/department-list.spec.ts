import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentList } from './department-list';

describe('DepartmentList', () => {
  let component: DepartmentList;
  let fixture: ComponentFixture<DepartmentList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DepartmentList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DepartmentList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
