import { Component, OnInit } from '@angular/core';
import { Department, DepartmentService } from '../services/department-service';
import { AuthService } from '../services/auth-service';

import { Observable } from 'rxjs';
import { ColDef } from 'ag-grid-community';

@Component({
  selector: 'app-department-list',
  standalone: false,
  templateUrl: './department-list.html',
  styleUrl: './department-list.css',
})
export class DepartmentList implements OnInit {


  //departments$!: Observable<Department[]>;

  //rowData: Department[] = [];
  rowData$!: Observable<Department[]>;

  columnDefs: ColDef<Department>[] = [
    {
      headerName: 'S.No',
      valueGetter: (params) => (params.node?.rowIndex ?? 0) + 1,
      width: 100
    },
    {
      field: 'name',
      sortable: true,
      filter: true,
      flex: 1
    },
    {
      field: 'code',
      sortable: true,
      filter: true,
      flex: 1
    },
    {
      headerName: 'Action',
      flex: 1,
      //cellRenderer: () => {
      //  return `<button class="edit-btn">Edit</button>`;
      //}
      cellRenderer: () => `<button class="edit-btn">Edit</button>`
    }
  ];


  constructor(private departmentService: DepartmentService, public authService: AuthService) { }

  //ngOnInit(): void {
  //  //this.departments$ = this.departmentService.getDepartments();
  //  //this.departmentService.getDepartments().subscribe(data => {
  //  //  this.rowData = data;
  //  //});
  //  //this.departmentService.getDepartments().subscribe(data => {
  //  //  this.rowData = [...data]; // triggers change detection properly
  //  //});


  //}

  ngOnInit(): void {
    this.rowData$ = this.departmentService.getDepartments();
  }



}
