import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';
import { FaIconLibrary, FontAwesomeModule } from '@fortawesome/angular-fontawesome'
import { faBeer, faHome, faUsers } from '@fortawesome/free-solid-svg-icons';
import { INavToggle, INavData } from '../../../models/sideNav.model';



@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, RouterModule, CommonModule, FontAwesomeModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

  @Output() onToggleNav: EventEmitter<INavToggle> = new EventEmitter<INavToggle>();

  title = 'Angular';
  screenWidth = 0;
  collapsed: boolean = true;
  navData:  INavData[] = [
    { routeLink: '/home', icon: faHome, label: 'Home' },
    { routeLink: '/halls', icon: faBeer, label: 'Halls' },
    { routeLink: '/contacts', icon: faUsers, label: 'Contacts' }
    
  ];
  readonly icons = [faHome, faBeer, faUsers]

  constructor(private library: FaIconLibrary) {
    this.library.addIcons(...this.icons);
   }

  toggleCollapsed(): void {
    this.collapsed = !this.collapsed;
    this.onToggleNav.emit({ screenWidth: this.screenWidth, collapsed: this.collapsed });
  }

  closeNavbar(): void {
    this.collapsed = false;
    this.onToggleNav.emit({ screenWidth: this.screenWidth, collapsed: this.collapsed });
  }
}
