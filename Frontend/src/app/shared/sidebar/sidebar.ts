import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-sidebar',
  standalone: false,
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css',
})
export class Sidebar implements OnInit {

  isLoggedIn$!: Observable<boolean>;

  constructor(public authService: AuthService) {}

  ngOnInit(): void {
    this.isLoggedIn$ = this.authService.loggedIn$;
  }

}
