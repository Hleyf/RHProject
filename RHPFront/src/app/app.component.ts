import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { JWT_OPTIONS, JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './components/auth/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  providers: [
    AuthService,
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS},
    JwtHelperService
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'RHPFront';
  
  constructor(private authService: AuthService, private router: Router) {
    if(authService.isLoggedIn()){
      console.log('User is logged in');
      router.navigate(['/home']);
    }else{
      router.navigate(['/login']);
    }
    console.log('AppComponent constructor');
  }
}
