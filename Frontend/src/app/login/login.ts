import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth-service'
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  loginForm !: FormGroup;
  error: boolean = false;
  submitted: boolean = false;

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router, private cdr: ChangeDetectorRef, private authService: AuthService, private toastr: ToastrService) {

    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    this.submitted = true;
    this.error = false;

    const formData = this.loginForm.value;

    this.authService.login(formData).subscribe({
      next: () => {
        console.log('Login successful');
        this.toastr.success("Loggedin successful");
        this.router.navigate(['/Employee/list']);
      },
      error: (err) => {
        console.error('Login failed', err);
        this.error = true;
        this.cdr.detectChanges();
      }
    });
  }

}
