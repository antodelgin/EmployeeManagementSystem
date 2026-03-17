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



const routes: Routes = [
  {
    path: 'Auth/validate',
    component: Login,
  },
  {
    path: 'Auth/sign-up',
    component: Signup
  },
  { path: 'home', component: Home },
  { path: 'Employee/list', component: EmployeeList, canActivate: [AuthGuard] },
  { path: 'Employee/create', component: EmployeeForm, canActivate: [AuthGuard, RoleGuard], data: { role: 'Admin' } },
  { path: 'Employee/update/:id', component: EmployeeForm, canActivate: [AuthGuard, RoleGuard], data: { role: 'Admin' } },
  { path: 'Employee/:id', component: EmployeeDetails, canActivate: [AuthGuard, RoleGuard], data: { role: 'Admin' } },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
  
}
