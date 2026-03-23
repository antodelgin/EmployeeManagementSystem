import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError, switchMap, take } from 'rxjs/operators';
import { AuthService } from './services/auth-service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(): Observable<boolean> {
    return this.authService.loggedIn$.pipe(
      take(1), // take latest value
      switchMap((isLoggedIn) => {
        if (isLoggedIn) {
          return of(true); 
        }

        return this.authService.checkAuth().pipe(
          map(() => true),
          catchError(() => {
            this.router.navigate(['/Auth/validate']);
            return of(false);
          })
        );
      })
    );
  }
}
