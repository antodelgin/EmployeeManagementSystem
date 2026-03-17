import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { EmployeeList } from './employee-list/employee-list';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { EmployeeDetails } from './employee-details/employee-details';
import { EmployeeForm } from './employee-form/employee-form';
import { Home } from './home/home';
import { Login } from './login/login';
import { Signup } from './signup/signup';
import { Header } from './shared/header/header';
//import { HTTP_INTERCEPTORS } from '@angular/common/http';
//import { AuthInterceptor } from './auth-interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { HttpClientModule } from '@angular/common/http';
import { DepartmentForm } from './department-form/department-form';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './auth-interceptor';



@NgModule({
  declarations: [
    App,
    EmployeeList,
    EmployeeDetails,
    EmployeeForm,
    Home,
    Login,
    Signup,
    Header,
    DepartmentForm
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule, 
    BrowserAnimationsModule,
    ToastrModule.forRoot()

  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(
      withInterceptors([authInterceptor])
    )

    //{
    //  provide: HTTP_INTERCEPTORS,
    //  useClass: AuthInterceptor,
    //  multi: true
    //}
  ],
  bootstrap: [App]
})
export class AppModule { }
