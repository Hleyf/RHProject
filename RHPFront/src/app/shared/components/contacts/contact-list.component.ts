import { Component, Signal} from '@angular/core';
import { SearchInputComponent } from '../search-input/search-input.component';
import { PlayerService } from '../../../services/player.service';
import { CommonModule } from '@angular/common';
import { Contact, UserPlayer, UserStatus } from '../../../models/player.model';
import { ContactListService } from '../../../services/contact-list.service';

export interface IContact {
  id: number;
  name: string;
  email: string;

}

@Component({
  selector: 'app-contact-list',
  standalone: true,
  imports: [CommonModule, SearchInputComponent],
  templateUrl: './contact-list.component.html',
  styleUrl: './contact-list.component.css'
})
export class ContactListComponent {
  
  readonly player: Signal<UserPlayer | null> = this.service.player;
  readonly contacts: Signal<Contact[]> = this.service.contactList; //fetching mock data atm.
  
  screenWidth = 0;
  collapsed!: boolean;
  userStatus = UserStatus;


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

  getStatusColour(status: UserStatus): string {
    switch (status) {
      case UserStatus.Online:
        return 'text-green-500';
      case UserStatus.Away:
        return 'text-yellow-500';
      case UserStatus.Offline:
        return 'text-gray-400';
      default:
        return 'text-gray-400';
    }
    }

}
