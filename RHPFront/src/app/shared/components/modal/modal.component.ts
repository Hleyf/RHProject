import { Component, ComponentRef, EventEmitter, OnDestroy, Output, Type, ViewChild, ViewContainerRef } from '@angular/core';

@Component({
  selector: 'app-modal',
  standalone: true,
  styleUrls: ['./modal.component.css'],
  template: `
    <div class="modal" [class.show]="show" tabindex="-1">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-body">
            <ng-container #modalContainer></ng-container>
          </div>
        </div>
      </div>
    </div>
  `,
})
export class ModalComponent implements OnDestroy {
  @ViewChild('modalContainer', { read: ViewContainerRef, static: true }) viewContainerRef!: ViewContainerRef;
  @Output() onClose = new EventEmitter<any>();
  private componentRef!: ComponentRef<any>;
  show: boolean = true;

  constructor() {}
  
  ngOnDestroy(): void {
    if (this.componentRef) {
      this.componentRef.destroy();
    }
  }

  open(component: Type<any>, data?: any) {
    this.componentRef = this.viewContainerRef.createComponent(component);
    this.componentRef.instance.data = data;
    this.show = true;	
  }

  close(data?: any) {
    if (this.componentRef) {
      this.onClose.emit(data);
      this.componentRef.destroy();
    }
    this.show = false;
  }


}

