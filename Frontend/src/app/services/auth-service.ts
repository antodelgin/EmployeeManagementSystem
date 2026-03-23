import { ChangeDetectorRef, inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable, BehaviorSubject, tap, catchError, of } from 'rxjs';

export interface LoginDto {
  email: string;
  password: string;
}


@Injectable({
  providedIn: 'root',
})
export class AuthService {
  
  private baseUrl = 'http://localhost:5000/auth';

  private loggedIn = new BehaviorSubject<boolean>(false);
  loggedIn$ = this.loggedIn.asObservable();
  private role = new BehaviorSubject<string | null>(null);
  role$ = this.role.asObservable();

  constructor(private http: HttpClient) { }

  login(data: any) {
    return this.http.post<any>(`${this.baseUrl}/validate`, data, { withCredentials: true })
      .pipe(
        tap(res => {
          this.loggedIn.next(true);
          this.role.next(res.role);
        })
      );
  }

  logout() {
    return this.http.post(`${this.baseUrl}/logout`, {}, { withCredentials: true })
      .pipe(
        tap(() => {
          this.loggedIn.next(false);
          this.role.next(null);
        })
      );
  }

  isLoggedIn() {
    return this.loggedIn.value;
  }

  getCurrentUser() {
    return this.http.get<any>(`${this.baseUrl}/current-user`);
  }

  getAuthStatus() {
    return this.loggedIn.value;
  }
  getRole(): string | null {
    return this.role.value;
  }

  initAuth() {
    this.http.get<any>(`${this.baseUrl}/current-user`, {
      withCredentials: true
    }).subscribe({
      next: (user) => {
        this.loggedIn.next(true);
        this.role.next(user.role);
      },
      error: () => {
        this.loggedIn.next(false);
        this.role.next(null);
      }
    });
  }

  checkAuth() {
    return this.http.get<any>(`${this.baseUrl}/current-user`, {
      withCredentials: true
    }).pipe(
      tap((user) => {
        this.loggedIn.next(true);
        this.role.next(user.role);
      })
    );
  }
  
}
