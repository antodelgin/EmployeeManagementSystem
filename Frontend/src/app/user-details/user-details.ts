import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth-service';

@Component({
  selector: 'app-user-details',
  standalone: false,
  templateUrl: './user-details.html',
  styleUrl: './user-details.css',
})
export class UserDetails {

  currentUser$!: Observable<any>;

  constructor(private authService: AuthService) {

  }

  ngOnInit(): void {
    this.currentUser$ = this.authService.getCurrentUser();

  }

}
