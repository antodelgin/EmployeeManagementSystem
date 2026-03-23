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

  isLoggedIn$!: Observable<boolean>;
  currentUser$!: Observable<any>;

  constructor(public authService: AuthService, private router: Router, private http: HttpClient) { }


  ngOnInit(): void {
    this.isLoggedIn$ = this.authService.loggedIn$; 

  }

  currentUser() {
    this.currentUser$ = this.authService.getCurrentUser();
  }


  logout() {
    this.authService.logout().subscribe(() => {
      this.router.navigate(['/Auth/validate']);
    });
  }

}
