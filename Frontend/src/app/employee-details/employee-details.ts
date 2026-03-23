import { Component, OnInit } from '@angular/core';
import { Employee,EmployeeService } from 'D:/CSharp/EmployeeManagementSystem/Frontend/src/app/services/employee-service';
import { Observable, switchMap, catchError, of } from 'rxjs'
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-employee-details',
  standalone: false,
  templateUrl: './employee-details.html',
  styleUrl: './employee-details.css',
})
export class EmployeeDetails implements OnInit  {

  employee$!: Observable<Employee | null> ;
  errorMessage: string = '';

  constructor(private employeeService: EmployeeService, private route: ActivatedRoute) {}
  ngOnInit(): void {

    this.employee$ = this.route.paramMap.pipe(
      switchMap(params => {
        const id = Number(params.get('id'));
        return this.employeeService.getEmployeeById(id).pipe(
          catchError(err => {
            if (err.error.message) {

              this.errorMessage = err.error.message;
            } else {
              this.errorMessage = 'An unexpected error occurred.';
            }
            return of(null);
          })
        );
      })
    );

  }


}
