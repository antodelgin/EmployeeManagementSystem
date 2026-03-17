import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  const router = inject(Router);

  const cloned = req.clone({
    withCredentials: true
  });

  return next(cloned).pipe(
    catchError((error: HttpErrorResponse) => {

      if (error.status === 401 && !req.url.includes('/auth/validate')) {
        router.navigate(['/Auth/validate']);
      }

      return throwError(() => error);
    })
  );
};



//import { HttpErrorResponse, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
//import { Injectable } from '@angular/core';
//import { Router } from '@angular/router';
//import { catchError, throwError } from 'rxjs';

////export const authInterceptor: HttpInterceptorFn = (req, next) => {
////  return next(req);
////};
//@Injectable()
//export class AuthInterceptor implements HttpInterceptor {

//  constructor(private router: Router) { }

//  intercept(req: HttpRequest<any>, next: HttpHandler) {
//    //console.log("Interceptor running");

//    const cloned = req.clone({
//      withCredentials: true
//    });

//    return next.handle(cloned).pipe(
//      catchError((error: HttpErrorResponse) => {

//        if (error.status === 401 && !req.url.includes('/auth/validate')) {
//          this.router.navigate(['/Auth/validate']);
//        }

//        return throwError(() => error);
//      })
//    );
//  }
//}
