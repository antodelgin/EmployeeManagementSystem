import { ChangeDetectorRef, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Department, DepartmentService } from '../services/department-service';
import { EmployeeService } from '../services/employee-service';

@Component({
  selector: 'app-department-form',
  standalone: false,
  templateUrl: './department-form.html',
  styleUrl: './department-form.css',
})
export class DepartmentForm {

  departmentCreationForm !: FormGroup;

  constructor(private fb: FormBuilder, private employeeService: EmployeeService, private departmentService: DepartmentService, private router: Router, private activatedRoute: ActivatedRoute, private cdr: ChangeDetectorRef) {

    this.departmentCreationForm = this.fb.group(
      {
        code: ['', Validators.required],
        name: ['', Validators.required]
      }
    )
  }
  
  onSubmit() {

    const formData: Department = this.departmentCreationForm.value;
    

    this.departmentService.createDepartment(formData).subscribe(
      (response) => {
        this.router.navigate(['/Department/list']);
        console.log(formData);

  })

  }

}
