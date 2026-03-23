
import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, combineLatest, debounceTime, distinctUntilChanged, map, Observable, switchMap } from 'rxjs';
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

    const debouncedSearch$ = this.search$.pipe(
      map(value => value.trim()),
      debounceTime(300),
      distinctUntilChanged()
    );

    this.employees$ = combineLatest([
      this.refresh$,
      this.departmentFilter$,
      debouncedSearch$
    ]).pipe(
      switchMap(([_, departmentId, name]) => {

        if (name.length > 0) {
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

  //searchEmployee() {
  //  this.pageNumber = 1;
  //  this.search$.next(this.employeeName);
  //}

  onSearchChange(value: string) {
    this.pageNumber = 1;
    this.search$.next(value);
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
