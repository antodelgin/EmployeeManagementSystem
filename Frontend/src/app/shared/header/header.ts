import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/employee-service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../services/auth-service';
import { Observable, tap } from 'rxjs';


@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header implements OnInit {

  //isLoggedIn: boolean = false;

  //constructor(private employeeService: EmployeeService, private router: Router, private http: HttpClient, private cdr: ChangeDetectorRef) {

  //}

  //ngOnInit(): void {
  //  // Check current auth state on init so the UI can reflect logged-in status
  //  this.http.get('http://localhost:5000/auth/check', { withCredentials: true }).subscribe({
  //    next: () => {
  //      this.isLoggedIn = true;
  //      this.cdr.detectChanges();

  //    },
  //    error: () => {
  //      this.isLoggedIn = false;
  //    }
  //  });
  //}

  ////isLoggedIn: boolean = true;
  //logout() {
  //  this.employeeService.logout().subscribe({
  //    next: () => {
  //      localStorage.clear();
  //      sessionStorage.clear();
  //      //this.isLoggedIn = false;
  //      this.router.navigate(['/Auth/validate']);
  //    },
  //    error: () => {
  //      //this.isLoggedIn = false;
  //      this.router.navigate(['/Auth/validate']);
  //    }
  //  });
  //}

  isLoggedIn$!: Observable<boolean>;
  currentUser$!: Observable<any>;



  constructor(public authService: AuthService, private router: Router, private http: HttpClient) { }


  ngOnInit(): void {
    //this.isLoggedIn$ = this.authService.authState$;  // assign here
    //this.authService.checkAuth();
    this.isLoggedIn$ = this.authService.loggedIn$; 

  }

  currentUser() {
    this.currentUser$ = this.authService.getCurrentUser();
  }


  logout() {
    this.authService.logout().subscribe(() => {
      //this.isLoggedIn$ = ;
      this.router.navigate(['/Auth/validate']);
    });
  }

  //logout(): Observable<any> {
  //  return this.http.post('/api/logout', {}).pipe(
  //    tap(() => {
  //      this.authService.loggedIn.next(false);
  //    })
  //  );
  //}
}
