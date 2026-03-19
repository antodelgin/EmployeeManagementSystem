
import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, combineLatest, map, Observable, switchMap } from 'rxjs';
import { Employee, EmployeeService } from '../services/employee-service';
//import { FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth-service';

@Component({
  selector: 'app-employee-list',
  standalone: false,
  templateUrl: './employee-list.html',
  styleUrls: ['./employee-list.css']
})
export class EmployeeList implements OnInit {

  employees$!: Observable<Employee[]>;
  pageNumber = 1;
  pageSize = 10;
  totalPages = 1;

  private refresh$ = new BehaviorSubject<void>(undefined);
  private departmentFilter$ = new BehaviorSubject<number | null>(null);

  private search$ = new BehaviorSubject<string>('');
  employeeName!: string;


  constructor(private employeeService: EmployeeService, public authService: AuthService) { }

  ngOnInit(): void {
    this.employees$ = combineLatest([
      this.refresh$,
      this.departmentFilter$,
      this.search$
    ]).pipe(
      switchMap(([_, departmentId, name]) => {

        if (name) {
          return this.employeeService.searchEmployeeByName(
            name,
            departmentId ?? 0,
            this.pageNumber,
            this.pageSize
          );
        }

        if (departmentId) {
          return this.employeeService.filterEmployeeByDepartmentId(
            departmentId,
            this.pageNumber,
            this.pageSize
          );
        }

        return this.employeeService.getAllEmployee(
          this.pageNumber,
          this.pageSize
        );
      }),
      map((response: any) => {
        this.totalPages = response.totalPages;
        return response.data;
      })
    );
  }

  //ngOnInit(): void {
  //  this.employees$ = combineLatest([this.refresh$, this.departmentFilter$]).pipe(
  //    switchMap(([_, departmentId]) => {

  //      if (departmentId) {
  //        return this.employeeService.filterEmployeeByDepartmentId(
  //          departmentId,
  //          this.pageNumber,
  //          this.pageSize
  //        );
  //      }

  //      return this.employeeService.getAllEmployee(
  //        this.pageNumber,
  //        this.pageSize
  //      );

  //    }),
  //    map((response: any) => {
  //      this.totalPages = response.totalPages;
  //      return response.data;
  //    })
  //  );
  //}

  //searchEmployee(name: string) {
  //  this.employeeService.searchEmployeeByName(name);
  //}

  //employeeName: string = '';
  //employees: any[] = [];

  //searchEmployee() {
  //  this.employeeService.searchEmployeeByName(this.employeeName);
  //    //.subscribe(data => {
  //    //  this.employeeName = data;
  //    //});
  //}
  searchEmployee() {
    this.pageNumber = 1;
    this.search$.next(this.employeeName);
  }

  deleteEmployee(id: number) {
    this.employeeService.deleteEmployeeById(id).subscribe({
      next: () => this.refresh$.next(),
      error: (err) => console.error('Delete failed', err)
    });
  }

  onFilterChange(id: number) {
    this.pageNumber = 1;
    this.departmentFilter$.next(id || null);
  }

  clearFilter() {
    this.pageNumber = 1;
    this.departmentFilter$.next(null);

    this.search$.next('');
    this.employeeName = '';
  }

  nextPage() {
    if (this.pageNumber < this.totalPages) {
      this.pageNumber++;
      this.refresh$.next();
    }
  }

  prevPage() {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.refresh$.next();
    }
  }
}
