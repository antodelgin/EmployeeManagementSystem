
import { ChangeDetectorRef, inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { Department } from './department-service';
//import { PagedResult } from '../employee-list/employee-list';

export interface Employee {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  departmentId: number;
  departmentName: string;
}

export interface CreateEmployee {
  firstName: string;
  lastName: string;
  email: string;
  departmentId: number;
}

export interface PagedResult<T> {
  data: T[];
  totalRecords: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}


@Injectable({
  providedIn: 'root', 
})
export class EmployeeService {

  http = inject(HttpClient)

  apiUrl = 'http://localhost:5000/Employee';

  isLoggedIn: boolean = true;

  createEmployee(employee: CreateEmployee): Observable<Employee> {
    //return this.http.post<Employee>(`${this.apiUrl}/create`, employee, { withCredentials : true });
    return this.http.post<Employee>(`${this.apiUrl}/create`, employee);

  }
  
  getEmployeeById(id: number): Observable<Employee> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  };

  //getAllEmployee(pageNumber: number, pageSize: number) {
  //  return this.http.get<any>(
  //    `${this.apiUrl}/list?pageNumber=${pageNumber}&pageSize=${pageSize}`
  //  );
  //}

  //getAllEmployee(): Observable<Employee[]> {

  //  return this.http.get<Employee[]>(`${this.apiUrl}/list`);
  //};

  getAllEmployee(pageNumber: number, pageSize: number): Observable<PagedResult<Employee>> {
    return this.http.get<PagedResult<Employee>>(
      `${this.apiUrl}/list?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
  }


  updateEmployee(id: number, employee: CreateEmployee): Observable<Employee> {
    return this.http.put<Employee>(`${this.apiUrl}/update/${id}`, employee);
  }


  deleteEmployeeById(id: number): Observable<any> {
    //console.log('hi...');
    return this.http.delete<any>(`${this.apiUrl}/delete/${id}`);
  }


  //filterEmployeeByDepartmentId(id:number) {
  //  return this.http.get<any>(`${this.apiUrl}/department/${id}`);
  //}

  filterEmployeeByDepartmentId(id: number, pageNumber: number, pageSize: number) {
    return this.http.get<any>(
      `${this.apiUrl}/department/${id}?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
  }

  //searchEmployeeByName(name: string) {
  //  return this.http.get<any>(
  //    `${this.apiUrl}/search?name=${name}`
  //  );
  //}
  searchEmployeeByName(name: string, departmentId: number, pageNumber: number, pageSize: number) {
    return this.http.get<any>(
      `${this.apiUrl}/search?name=${name}&departmentId=${departmentId}&pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
  }

  logout() {

    //this.isLoggedIn = false;
    return this.http.post<any>(
      'http://localhost:5000/auth/logout',
      {},
      { withCredentials: true }
    );

  }
}


