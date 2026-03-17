import { ChangeDetectorRef, Component,Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EmployeeService, CreateEmployee } from '../services/employee-service';
import { DepartmentService } from '../services/department-service';
import { ActivatedRoute,Router } from '@angular/router';



@Component({
  selector: 'app-employee-form',
  standalone: false,
  templateUrl: './employee-form.html',
  styleUrl: './employee-form.css',
})
export class EmployeeForm {

  employeeCreationForm !: FormGroup;
  isEdit = false;
  employeeId!: number;
  errorMessage: string = '';
  departments: Array<{ id: number; name: string }> = [];

  constructor(private fb: FormBuilder, private employeeService: EmployeeService, private departmentService: DepartmentService, private router: Router, private activatedRoute: ActivatedRoute, private cdr: ChangeDetectorRef) {

    this.employeeCreationForm = this.fb.group(
      {
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        departmentId: ['', Validators.required]
      }
    )
  }
  ngOnInit(): void{

    this.departmentService.getDepartments().subscribe({
      next: (data) => {
        this.departments = data;
        this.cdr.detectChanges();
      },
      error: () => this.errorMessage = 'Failed to load departments'
    });

     const idParam = this.activatedRoute.snapshot.paramMap.get('id');

     if (idParam) {
       this.isEdit = true;
       this.employeeId = Number(idParam);

       this.employeeService.getEmployeeById(this.employeeId)
        .subscribe(employee => {
          this.employeeCreationForm.patchValue(employee);
        });
     }
  }

onSubmit() {
    //this.errorMessage = '';

    if (this.employeeCreationForm.invalid) {
      return;
    }
      

    const formData: CreateEmployee = this.employeeCreationForm.value;

      if (this.isEdit) {
        this.employeeService.updateEmployee(this.employeeId, formData)
          .subscribe({
            next: (response) => {
              console.log(response);
              this.router.navigate(['/Employee/list']);
            },
            error: (error) => {
              this.errorMessage = error.error?.message || 'An unexpected error occurred';
              console.error(this.errorMessage);
              this.cdr.detectChanges();
            }
          });
      } else {

        this.employeeService.createEmployee(formData).subscribe({
          next: (response) => {
            //console.log(response);
            //this.errorMessage = response.message || 'Employee created successfully!';
            this.router.navigate(['/Employee/list']);
          },
          error: (error) => {
            this.errorMessage = error.error.message;
            console.log(this.errorMessage);
            this.cdr.detectChanges();
          }
        })
      }
}




  //updateError = (errorMsg: string) => {
  //  console.log(this);
  //  console.log(this.errorMessage);
  //  this.errorMessage = errorMsg;
  //
}
