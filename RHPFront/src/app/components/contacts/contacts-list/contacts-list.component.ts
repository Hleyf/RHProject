import { Component, Signal, computed } from '@angular/core';
import { SearchInputComponent } from '../../../shared/components/search-input/search-input.component';
import { PlayerService } from '../../../services/player.service';
import { IContactLittle } from '../../../models/player.model';

@Component({
  selector: 'app-contacts-list',
  standalone: true,
  imports: [SearchInputComponent],
  templateUrl: './contacts-list.component.html',
  styleUrl: './contacts-list.component.css'
})
export class ContactsListComponent {

//? When the component loads I want to display the user's contact list. 
//? Later we can manage the search in different ways, I am not sure how right now. 
  entityList: Signal<IContactLittle[]> = computed(() => this.service.contactList()
    .map(contact => ({
    userId: contact.userId,
    name: contact.name,
    isContact: true
  }))); 

  constructor(private service: PlayerService) { 
    service.getPlayerContacts();

  }

  searchPlayers(event: string) {
    console.log(event);
  }
}
