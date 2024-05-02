import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../../shared/constants';
import { Observable, catchError, tap, throwError } from 'rxjs'
import { CookieService } from 'ngx-cookie-service';

export interface AuthResponse {
  token: string;
}

export class AuthRequest {
  username: string;
  password: string;

  constructor(req : any){
    this.username = req.username;
    this.password = req.password;
  }
}

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  constructor(private http: HttpClient, private cookieService: CookieService ) {}

  login(credentials : AuthRequest): Observable<string> {
    return this.http.post(API_URL + '/auth/login', credentials, {responseType: 'text'})
    .pipe(tap(res => {
      this.cookieService.set('token', res);
    }), catchError(err => {
        if(err.ok) {
            return throwError(() => new Error(err.error));
        }
        else {
          return throwError(() => new Error('Something went wrong during login'));
        }
    }));
}

  isLoggedIn() {
    const token = this.cookieService.get('token');
    return !!token;
  }
  
  logout() {
  this.cookieService.delete('token');
}
}
