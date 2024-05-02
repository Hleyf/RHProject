import { Component, Signal, ViewContainerRef, computed } from '@angular/core';
import { HallService } from '../../../services/hall.service';
import { IHall } from '../../../models/hall.model';
import { Router } from '@angular/router';
import { ModalService } from '../../../services/modal.service';
import { HallPreviewComponent } from '../modals/hall-preview/hall-preview.component';

@Component({
  selector: 'app-hall-list',
  standalone: true,
  imports: [],
  templateUrl: './hall-list.component.html',
  styleUrl: './hall-list.component.css'
})
export class HallListComponent {

  loading : Signal<boolean> = computed(() => {return this.service.hallsLoading()});
  readonly entities: Signal<IHall[]> = computed(() => {return this.service.halls()});

  constructor(
    private service: HallService, 
    private router: Router, 
    private modalService: ModalService,
    private viewRef: ViewContainerRef
  )
    {
    this.service.getHalls();
    }

  goToHall(id: number) {
    this.router.navigate(['/hall', id]);
  }


  openPreviewModal(id: number){
    const data = {id: id};
    this.modalService.open(this.viewRef, HallPreviewComponent, data).onClose(data => {
    });
  }
   

}
