import { Component, Signal, ViewContainerRef, computed } from '@angular/core';
import { HallService } from '../../../services/hall.service';
import { Hall, IHall } from '../../../models/hall.model';
import { Router } from '@angular/router';
import { ModalService } from '../../../services/modal.service';
import { HallPreviewComponent, IHallPreview } from '../modals/hall-preview/hall-preview.component';

@Component({
  selector: 'app-hall-list',
  standalone: true,
  imports: [],
  templateUrl: './hall-list.component.html',
  styleUrl: './hall-list.component.css'
})
export class HallListComponent {

  loading : Signal<boolean> = computed(() => {return this.service.isHallsLoading()});
  readonly entities: Signal<Hall[]> = computed(() => {return this.service.halls()});

  constructor(
    private service: HallService, 
    private router: Router, 
    private modalService: ModalService,
    private viewContainerRef: ViewContainerRef
  )
    {
    this.service.getHalls();
    this.modalService.setViewContainerRef(this.viewContainerRef);
    }

  goToHall(id: number) {
    this.router.navigate(['/hall', id]);
  }


  openPreviewModal(id: number){
    const data = {id: id};
    this.modalService.open({
      component:HallPreviewComponent, 
      data: data,
      options: {
        width: '500px', 
        height: '700px',
        header: false
      }
    }).onClose((result : IHallPreview) => {
    });
  }
   

}
