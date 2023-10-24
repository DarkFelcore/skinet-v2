import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, catchError, delay, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(
    private router: Router,
    private toastr: ToastrService
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    
    return next.handle(request).pipe(
      catchError((err: any) => {
        if(err) {
          if(err.error.status === 400) {
            var errorsMessages = Object.values(err.error.errors).join(', ')
            this.toastr.error(errorsMessages)
          }
          if(err.error.status === 401) {
            this.toastr.error("Unauthorized")
          }
          if(err.error.status === 404) {
            const navigateionExtras: NavigationExtras = {state: {error: err.error}}
            this.router.navigateByUrl('/not-found', navigateionExtras)
            this.toastr.error(err.error.title);
          }
          if(err.error.status === 500) {
            const navigateionExtras: NavigationExtras = {state: {error: err.error}}
            this.router.navigateByUrl('/server-error', navigateionExtras);
          }
          // In case backend is turned off
          // Only for development !!
          if(err.status === 0) {
            this.router.navigateByUrl('/server-error');
            this.toastr.warning("Your backend is not running or maybe another error")
          }
        }
        return throwError(() => err);
      })
    )

  }
}
