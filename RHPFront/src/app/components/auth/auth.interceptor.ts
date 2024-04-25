import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authToken = localStorage.getItem('token');

  const authReq = req.clone({
    setHeaders: {
      Authorization: `Bearer ${authToken}`
    }
  });
  return next(authReq).pipe(
    catchError((err: any) => {
      if(err instanceof HttpErrorResponse){
        // Errors are handled here
        if(err.status === 401){
          console.error('Unauthorized request', err);
          // localStorage.removeItem('token'); // Whe are just recording the errors at the moment
        }
      } else {
        console.error('An unexpected error occurred', err);
      }
      return throwError(() => err);
    })
  );
};
