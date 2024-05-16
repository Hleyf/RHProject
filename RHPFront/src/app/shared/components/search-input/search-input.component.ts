import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faClose } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-sesarch-input',
  standalone: true,
  imports: [CommonModule, FormsModule,FontAwesomeModule],
  template: `
    <div>
      @if(showLabel){
        <label class="search-label">{{ label }}</label>
      }
      <input
        id="search-box"
        type="text"
        [placeholder]="placeholder"
        [(ngModel)]="searchTerm"
        (keyup.enter)="onSearch()"
      />
      @if (showClear) {
      <fa-icon class="clear-icon" [icon]="closeIcon" (click)="clearSearch()"></fa-icon>
      }
      <!-- <button (click)="onSearch()">Search</button>  -->
      <!-- TODO: removable tag for reseting searches -->
    </div>
  `,
  styleUrl: './search-input.component.css',
})
export class SearchInputComponent {
  @Input() searchFn!: (searchTerm: string) => void;
  @Input() label: string = '';
  @Input() placeholder: string = '';
  @Input() showLabel: boolean = true
  @Input() showClear: boolean = true

  searchTerm: string = '';
  closeIcon = faClose;

  constructor() {}

  onSearch(): void {
    this.searchFn(this.searchTerm);
  }

  clearSearch() {
    this.searchTerm = '';
  }
}
