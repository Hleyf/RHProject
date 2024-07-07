import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AuthService } from './components/auth/auth.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { TopNavbarComponent } from './shared/components/top-navbar/top-navbar.component';
import { ContactsBarService } from './services/contact-bar.service';
import { ContacsSidebarComponent } from './shared/components/contacts/contacts-sidebar/contacts-sidebar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, HttpClientModule, ContacsSidebarComponent, TopNavbarComponent  ],
  providers: [
    AuthService,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {

  title = 'RHPFront';
  screenWidth: number = 0;
  isNavCollapsed: boolean = true;
  
  constructor(protected authService: AuthService, private router: Router, private contactListService: ContactsBarService) {

    if(this.authService.isLoggedIn()){
      this.router.navigate(['/home']);
    }else{
      this.router.navigate(['/login']);
    }

    this.contactListService.isCollapsed.subscribe((collapsed: boolean) => {
      this.isNavCollapsed = collapsed;
    });

  }



  isLooginOrUserCreate(){
    const url = this.router.url;
    return url === '/login' || url === '/new-user';
  }

  
//TODO: I don't think this will be needed anymore. 
//   getContentClass(): string {
//   let style = '';
//   if(this.isNavCollapsed && this.screenWidth > 768){
//     style = 'content-collapsed-mobile';
//   }else if(this.isNavCollapsed && this.screenWidth <= 768){
//     style = 'content-collapsed';
//   }
//   return style;
// }
}
