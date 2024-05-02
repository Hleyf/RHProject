import { ComponentRef, Injectable, Type, ViewContainerRef } from '@angular/core';
import { ModalComponent } from '../shared/components/modal/modal.component';

@Injectable({
  providedIn: 'root'
})
export class ModalService {
  private modalRef!: ComponentRef<ModalComponent>
  private onCloseCallback: ((data: any) => void) | null = null;
  
  constructor(){}

  open(viewRef: ViewContainerRef, component: Type<any>, data?: any): { onClose: (callback: (data: any) => void) => void } {
    console.log('opening modal');
    //Create a new modal component
    this.modalRef = viewRef.createComponent(ModalComponent);

    this.modalRef.instance.onClose.subscribe((data: any) => {
      if (this.onCloseCallback) {
        this.onCloseCallback(data);
      }
    });

    // Create the content component inside the ModalComponent
    this.modalRef.instance.open(component, data);

    return {
      onClose: (callback: (data: any) => void) => {
        this.onCloseCallback = callback;
      }
    }
  }

  close(data?: any) {
    if(this.modalRef){
      this.modalRef.destroy();
    }
  }
}
