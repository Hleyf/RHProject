import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../../shared/constants';
import { Observable, tap } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt'

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

  constructor(private http: HttpClient, private jwHelper: JwtHelperService ) {}

  login(credentials : AuthRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(API_URL + '/auth/login', credentials)
    .pipe(tap(res => {
      localStorage.setItem('token', res.token); 
    }));
  }

  isLoggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwHelper.isTokenExpired(token);
  }
  
  logout() {
    localStorage.removeItem('token');
  }
}
