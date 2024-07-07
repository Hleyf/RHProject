import { Component, Input } from '@angular/core';
import {
  Contact,
  UserStatus,
  ContactStatus,
} from '../../../../models/player.model';
import { CommonModule } from '@angular/common';
import { ContactListService } from '../../../../services/contact-list.service';

@Component({
  selector: 'app-contact-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './contact-card.component.html',
  styleUrl: './contact-card.component.css',
})
export class ContactCardComponent {
  @Input() contact!: Contact;
  userStatus = UserStatus;
  contactStatus = ContactStatus;
  isCollapsed = true;

  constructor(private service: ContactListService) {
    this.service.isCollapsed.subscribe((value) => (this.isCollapsed = value));
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
