import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-sesarch-input',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div>
      <label class="search-label">{{ label }}</label>
      <input
        id="search-box"
        type="text"
        [placeholder]="placeholder"
        [(ngModel)]="searchTerm"
        (keyup.enter)="onSearch()"
      />
      @if (searchTerm) {
      <button *ngIf="searchTerm" class="clear-icon" (click)="clearSearch()">
        X
      </button>

      }
      <button (click)="onSearch()">Search</button>
    </div>
  `,
  styleUrl: './sesarch-input.component.css',
})
export class SesarchInputComponent {
  @Input() searchFn!: (searchTerm: string) => void;
  @Input() label!: string;
  @Input() placeholder!: string;

  searchTerm: string = '';

  constructor() {}

  onSearch(): void {
    this.searchFn(this.searchTerm);
  }

  clearSearch() {
    this.searchTerm = '';
  }
}
