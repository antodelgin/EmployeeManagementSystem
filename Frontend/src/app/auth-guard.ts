import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { AuthService } from './services/auth-service';

//@Injectable({
//  providedIn: 'root'
//})
//export class AuthGuard implements CanActivate {

//  constructor(private http: HttpClient, private router: Router) { }

//  canActivate(
//    route: ActivatedRouteSnapshot,
//    state: RouterStateSnapshot
//  ): Observable<boolean> {

//    return this.http.get('http://localhost:5000/auth/check', { withCredentials: true }).pipe(
//      map(() => true), // authenticated
//      catchError(() => {
//        this.router.navigate(['/Auth/validate']);
//        return of(false);
//      })
//    );
//  }
//}

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  //constructor(private router: Router) { }

  //canActivate(): boolean {

  //  // allow route access
  //  return true;

  //}

  constructor(private auth: AuthService, private router: Router) { }

  canActivate(): boolean {

    if (this.auth.getAuthStatus()) {
      return true;
    }

    this.router.navigate(['/Auth/validate']);
    return false;
  }

}
//@Injectable({
//  providedIn: 'root'
//})
//export class AuthGuard implements CanActivate {

//  constructor(private router: Router) { }

//  canActivate(): boolean {

//    // Optional simple client check
//    // (for example, checking if login page should be blocked)

//    return true;

//  }
//}
