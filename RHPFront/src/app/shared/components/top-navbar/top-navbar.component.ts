import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ContactsBarService } from '../../../services/contact-bar.service';

interface NavItem {
  label: string;
  url: string;
  icon?: string;
}

@Component({
  selector: 'app-top-navbar',
  standalone: true,
  imports: [RouterLink, RouterModule, CommonModule, FontAwesomeModule],
  templateUrl: './top-navbar.component.html',
  styleUrl: './top-navbar.component.css'
})
export class TopNavbarComponent {

  constructor(private contactListService: ContactsBarService){}

  navItems: NavItem[] = [
    { label: 'Home', url: '/home'},
    { label: 'Contacts', url: '/contacts' }
  ]

  toggleCollapsed(): void {
    this.contactListService.toggleCollapsed();
  }

}
