import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AuthService } from './components/auth/auth.service';
import { ISideElementToggle } from './models/sideNav.model';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ContactListComponent } from './shared/components/contacts/contact-list.component';
import { TopNavbarComponent } from './shared/components/top-navbar/top-navbar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, HttpClientModule, ContactListComponent, TopNavbarComponent  ],
  providers: [
    AuthService,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {

  title = 'RHPFront';
  
  constructor(protected authService: AuthService, private router: Router) {

    if(this.authService.isLoggedIn()){
      this.router.navigate(['/home']);
    }else{
      this.router.navigate(['/login']);
    }
  }

  screenWidth: number = 0;
  isNavCollapsed: boolean = true;

  isLooginOrUserCreate(){
    const url = this.router.url;
    return url === '/login' || url === '/user-create';
  }

  onToggleNav(data: ISideElementToggle): void {
   this.isNavCollapsed = data.collapsed;
   this.screenWidth = data.screenWidth;
  }

  getContentClass(): string {
  let style = '';
  if(this.isNavCollapsed && this.screenWidth > 768){
    style = 'content-collapsed-mobile';
  }else if(this.isNavCollapsed && this.screenWidth <= 768){
    style = 'content-collapsed';
  }
  return style;
}
}
