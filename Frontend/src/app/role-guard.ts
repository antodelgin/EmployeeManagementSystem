import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { AuthService } from './services/auth-service';


@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {



  constructor(private auth: AuthService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {

    const role = this.auth.getRole();
    const expectedRole = route.data['role'];

    if (role === expectedRole) {
      return true;
    }

    this.router.navigate(['/home']);
    return false;
  }
}

