import { Component, Signal, computed} from '@angular/core';
import { SearchInputComponent } from '../../../shared/components/search-input/search-input.component';
import { PlayerService } from '../../../services/player.service';

export interface IContact {
  id: number;
  name: string;
  email: string;

}

@Component({
  selector: 'app-contact-list',
  standalone: true,
  imports: [SearchInputComponent],
  templateUrl: './contact-list.component.html',
  styleUrl: './contact-list.component.css'
})
export class ContactListComponent {

  readonly entities: Signal<IContact[]> = this.service.contactList;
  constructor(private service: PlayerService) {
    this.service.getPlayerContacts();
  }

  searchContacts(searchTerm: string): void {
    this.service.searchContacts(searchTerm);
  }

}
