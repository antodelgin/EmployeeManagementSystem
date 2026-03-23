import { Component, signal } from '@angular/core';
import { AuthService } from './services/auth-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Frontend');

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.authService.initAuth(); 
  }
}
