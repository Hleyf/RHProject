import { Component, EventEmitter, Output, Signal, computed} from '@angular/core';
import { SearchInputComponent } from '../search-input/search-input.component';
import { PlayerService } from '../../../services/player.service';
import { ISideElementToggle } from '../../../models/sideNav.model';
import { CommonModule } from '@angular/common';

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
  @Output() onToggleNav: EventEmitter<ISideElementToggle> = new EventEmitter<ISideElementToggle>();
  screenWidth = 0;
  collapsed: boolean = true;

  readonly entities: Signal<IContact[]> = this.service.contactList;
  constructor(private service: PlayerService) {
    this.service.getPlayerContacts();
  }

  searchContacts(searchTerm: string): void {
    this.service.searchContacts(searchTerm);
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
