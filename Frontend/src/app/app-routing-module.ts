import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeDetails } from './employee-details/employee-details';
import { Home } from './home/home';
import { EmployeeList } from './employee-list/employee-list';
import { EmployeeForm } from './employee-form/employee-form';
import { Login } from './login/login';
import { Signup } from './signup/signup';
import { AuthGuard } from './auth-guard';
import { RoleGuard } from './role-guard';
import { DepartmentForm } from './department-form/department-form';
import { DepartmentList } from './department-list/department-list';
import { UserDetails } from './user-details/user-details';



const routes: Routes = [
  //{
  //  path: 'Auth/validate',
  //  component: Login,
  //},
  //{
  //  path: 'Auth/sign-up',
  //  component: Signup
  //},
  ////{ path: 'home', component: Home },
  //{
  //  path: 'Auth/current-user', component: UserDetails, canActivate: [AuthGuard]
  //},
  //{ path: 'Employee/list', component: EmployeeList, canActivate: [AuthGuard] },
  //{ path: 'Employee/create', component: EmployeeForm, canActivate: [AuthGuard, RoleGuard], data: { role: 'Admin' } },
  //{ path: 'Employee/update/:id', component: EmployeeForm, canActivate: [AuthGuard, RoleGuard], data: { role: 'Admin' } },
  //{ path: 'Employee/:id', component: EmployeeDetails, canActivate: [AuthGuard, RoleGuard], data: { role: 'Admin' } },
  //{ path: 'Department/create', component: DepartmentForm, canActivate: [AuthGuard, RoleGuard], data: { role: 'Admin' } },
  //{ path: 'Department/list', component: DepartmentList, canActivate: [AuthGuard] }
  {
    path: 'Auth',
    children: [
      { path: 'validate', component: Login },
      { path: 'sign-up', component: Signup },
      { path: 'current-user', component: UserDetails, canActivate: [AuthGuard] }
    ]
  },
  {
    path: 'Employee',
    children: [
      { path: 'list', component: EmployeeList },
      { path: 'create', component: EmployeeForm, canActivate: [RoleGuard], data: { role: 'Admin' } },
      { path: 'update/:id', component: EmployeeForm, canActivate: [RoleGuard], data: { role: 'Admin' } },
      { path: ':id', component: EmployeeDetails, canActivate: [RoleGuard], data: { role: 'Admin' } }
    ]
  },
  {
    path: 'Department',
    canActivate: [AuthGuard],
    children: [
      { path: 'list', component: DepartmentList },
      { path: 'create', component: DepartmentForm, canActivate: [RoleGuard], data: { role: 'Admin' } }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
  
}
