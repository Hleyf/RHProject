import { Component, EventEmitter, Injector, Output, Type, ViewChild, ViewContainerRef } from '@angular/core';
import { IModalOptions } from '../../../services/modal.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-modal',
  standalone: true,
  styleUrls: ['./modal.component.css'],
  imports: [CommonModule],
  template: `
    <div class="modal" [class.show]="show" tabindex="-1" (click)="destroy()">
      <div class="modal-overlay">
      <div class="modal-dialog" (click)="$event.stopPropagation()">          
        <div class="modal-content" [style.width]="width" [style.height]="height">
          @if (header) {
            <header class="modal-header">
              <h3 class="modal-title">{{title}}</h3>
              <button type="button" class="btn-close" (click)="destroy()"></button>
            </header>
          }
            <section class="modal-body">
              <ng-container #modalContainer></ng-container>
            </section>
          </div>
        </div>
      </div>
    </div>
  `,
})
export class ModalComponent {
  @ViewChild('modalContainer', { read: ViewContainerRef, static: true }) viewContainerRef!: ViewContainerRef;
  @Output() onClose = new EventEmitter<any>();
  @Output() onDestroy = new EventEmitter<any>();

  show: boolean = true;
  width: string = 'auto';
  height: string = 'auto';
  title: string = '';
  header: boolean = false;

  ngOnDestroy() {
    this.viewContainerRef.clear();
  }

  open(component: Type<any>, injector: Injector, options?: IModalOptions) {
    this.viewContainerRef.clear();

    this.setOptions(options);

    this.viewContainerRef.createComponent(component, {injector})
    this.show = true;	
  }
  
  close(data?: any) {
    this.onClose.emit(data);
    this.viewContainerRef.clear();
    this.show = false;
  }
  
  destroy() {
    this.viewContainerRef.clear();
    this.show = false;
    this.onDestroy.emit();
  }
  
  private setOptions(options: IModalOptions | undefined) {
    if (!options) {
      return;
    }
  
    const { width, height, title, header } = options;
  
    if (width) {
      this.width = width;
    }
  
    if (height) {
      this.height = height;
    }

    if(header) {
      this.header = header;
    }
      if (title) {
        this.title = title;
      }
  }
}


