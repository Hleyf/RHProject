import { ComponentRef, Injectable, InjectionToken, Injector, Type, ViewContainerRef } from '@angular/core';
import { ModalComponent } from '../shared/components/modal/modal.component';
import { Subject, takeUntil } from 'rxjs';

export const MODAL_DATA = new InjectionToken<unknown>('ModalData');

export interface IModalData {
  component: Type<any>;
  data?: any;
  options?: IModalOptions
}
export interface IModalOptions {
  width?: string;
  height?: string;
  header?: boolean;
  title?: string;
}

/*
* ModalService is a service that allows you to open a modal window with a component inside.
* The modal window is created by the ModalComponent.
* The ModalService creates a new instance of the ModalComponent and injects the component you want to display inside the modal window.
* To share data between the component and the modal window, you can use the MODAL_DATA injection token.
* The shared data is passed through injecting the MODAL_DATA token into the component's constructor. 
* example: 
*    constructor(@Inject(MODAL_DATA) public data: T) { }
*/

@Injectable({
  providedIn: 'root'
})
export class ModalService {
  private viewContainerRef!: ViewContainerRef;
  private modalRef!: ComponentRef<ModalComponent>
  private onCloseCallback: ((data: any) => void) | null = null;
  private destroy$ = new Subject<void>();
  
  constructor(private injector: Injector) {}
  
  setViewContainerRef(viewContainerRef: ViewContainerRef) {
    this.viewContainerRef = viewContainerRef;
  }

  open<T>(modalData: IModalData): { onClose: (callback: (result: any) => void) => void } {
    
    if (!this.checkViewContainerRef()) {
      return { onClose: () => {} };
    }

    const childInjector = this.createChildInjector(modalData.data);

    //Only one modal can be opened at a time
    if(!this.modalRef){
      this.destroy();
    }

    this.createModalComponent(childInjector);
    this.openContentComponent(modalData.component, childInjector, modalData.options);
    this.subscribeToCloseEvent();
    this.subscribeToDestroyEvent();

    return this.getOnCloseObject();
}

private checkViewContainerRef(): boolean {
    if (!this.viewContainerRef) {
      console.error('viewContainerRef is not set. Please call setViewContainerRef before calling open.');
      return false;
    }
    return true;
}

private createChildInjector<T>(data?: T): Injector {
    const providers = [{ provide: MODAL_DATA, useValue: data ?? {}}];
    return Injector.create({providers, parent: this.injector});
}

private createModalComponent(childInjector: Injector): void {
    this.modalRef = this.viewContainerRef.createComponent(ModalComponent, {injector: childInjector});
}

private openContentComponent(component: Type<any>, childInjector: Injector, options?: IModalOptions): void {
    this.modalRef.instance.open(component, childInjector, options );
}

private subscribeToCloseEvent(): void {
    this.modalRef.instance.onClose
      .pipe(takeUntil(this.destroy$))
      .subscribe((result: any) => {
        if (this.onCloseCallback) {
          this.onCloseCallback(result);
        }
      });
}

private subscribeToDestroyEvent(): void {
    this.modalRef.instance.onDestroy
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
      this.destroy();
    });
}

private getOnCloseObject(): { onClose: (callback: (result: any) => void) => void } {
    return {
      onClose: (callback: (result: any) => void) => {
        this.onCloseCallback = callback;
      }
    };
}

  close(result?: any) {
    if(result && this.onCloseCallback){
      this.onCloseCallback(result);
    }

    this.destroy();
  }

  destroy(){
    if(this.modalRef){
      this.modalRef.destroy();
    }
    this.destroy$.next();
    this.destroy$.complete();
  }
}
