import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {Router} from '@angular/router';
import {Injectable} from '@angular/core';
import {catchError} from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor{
  constructor(private router: Router) {
  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(err => {
        if(err){
          if(err.status === 404){
            console.log("not found error", err);
          }
          if(err.status === 500){
            this.router.navigateByUrl('/home');
          }
        }
        return throwError(err);
      })
    );
  }

}
