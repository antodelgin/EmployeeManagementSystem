import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-signup',
  standalone: false,
  templateUrl: './signup.html',
  styleUrl: './signup.css',
})
export class Signup {

  submitted: boolean = false;
  signUpForm !: FormGroup;
  success: boolean = false;


  constructor(private fb: FormBuilder, private http: HttpClient) {

    this.signUpForm = this.fb.group({
      username: [''],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      role: ['', Validators.required]
    });
  }

  onSubmit() {
    this.submitted = true;

    if (this.signUpForm.invalid) {
      return;
    }

    const formData = this.signUpForm.value;

    const apiUrl = 'http://localhost:5000/auth/sign-up';

    this.http.post(apiUrl, formData).subscribe({
      next: (response) => {
        console.log('SignIn successful', response);
        this.success = true;

        this.signUpForm.reset();
        this.submitted = false;
      },
      error: (err) => {
        console.error('SignIn failed', err);
        this.success = false;
      }
    })

  }

}
