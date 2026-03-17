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
  //http = inject(HttpClient)

  //apiUrl = 'http://localhost:5000/auth/validate';

  //login(formData: LoginDto): Observable<any> {
  //  return this.http.post(`${this.apiUrl}/validate`, formData, { withCredentials: true });
  //}

  //createEmployee(employee: CreateEmployee): Observable<Employee> {
  //  return this.http.post<Employee>(`${this.apiUrl}/create`, employee, { withCredentials: true });

  //}

  //private http = inject(HttpClient);
  //private baseUrl = 'http://localhost:5000/auth';

  //// Central auth state
  //private authState = new BehaviorSubject<boolean>(false);
  //authState$ = this.authState.asObservable();

  //login(formData: LoginDto): Observable<any> {
  //  return this.http.post<any>(`${this.baseUrl}/validate`, formData, { withCredentials: true }).pipe(
  //    tap(() => this.authState.next(true))
  //  );
  //}

  //checkAuth(): void {
  //  this.http.get<any>(`${this.baseUrl}/check`, { withCredentials: true }).pipe(
  //    tap(() => this.authState.next(true)),
  //    catchError(() => {
  //      this.authState.next(false);
  //      return of(null);
  //    })
  //  ).subscribe();
  //}

  //logout(): Observable<any> {
  //  return this.http.post<any>(`${this.baseUrl}/logout`, {}, { withCredentials: true }).pipe(
  //    tap(() => this.authState.next(false))
  //  );
  //}
  private baseUrl = 'http://localhost:5000/auth';

  //private authState = new BehaviorSubject<boolean>(false);
  //authState$ = this.authState.asObservable();

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

  setLoggedIn(value: boolean) {
    this.loggedIn.next(value);
  }

  getAuthStatus() {
    return this.loggedIn.value;
  }
  getRole(): string | null {
    return this.role.value;
  }
  //checkAuth() {
  //  this.http.get('http://localhost:5000/auth/check', { withCredentials: true })
  //    .subscribe({
  //      next: () => this.authState.next(true),
  //      error: () => this.authState.next(false)
  //    });
  //}

  
}
