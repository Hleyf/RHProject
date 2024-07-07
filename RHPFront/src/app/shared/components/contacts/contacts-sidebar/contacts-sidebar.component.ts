import { Component, Signal} from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserPlayer, Contact } from '../../../../models/player.model';
import { ContactListService } from '../../../../services/contact-list.service';
import { PlayerService } from '../../../../services/player.service';
import { SearchInputComponent } from '../../search-input/search-input.component';
import { ContactCardComponent } from '../contact-card/contact-card.component';


export interface IContact {
  id: number;
  name: string;
  email: string;

}

@Component({
  selector: 'app-contacts-sidebar',
  standalone: true,
  imports: [CommonModule, SearchInputComponent, ContactCardComponent],
  templateUrl: './contacts-sidebar.component.html',
  styleUrl: './contacts-sidebar.component.css'
})
export class ContacsSidebarComponent {
  
  readonly player: Signal<UserPlayer | null> = this.service.player;
  readonly contacts: Signal<Contact[]> = this.service.contactList; //fetching mock data atm.
  
  screenWidth = 0;
  collapsed!: boolean;

  constructor(private service: PlayerService, private contactListService: ContactListService) {
    this.service.getPlayerContacts();
    this.contactListService.isCollapsed.subscribe(collapsed => {
      this.collapsed = collapsed;
    });
  }

  searchContacts(searchTerm: string): void {
    this.service.searchContacts(searchTerm);
  }

  toggleCollapsed(): void {
    this.contactListService.toggleCollapsed();
    
  }

}
