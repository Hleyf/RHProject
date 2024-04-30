import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AuthService } from './components/auth/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  providers: [
    AuthService,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'RHPFront';
  
  constructor(private authService: AuthService, private router: Router) {
    if(this.authService.isLoggedIn()){
      console.log('User is logged in');
      this.router.navigate(['/home']);
    }else{
      this.router.navigate(['/login']);
    }
  }
}
