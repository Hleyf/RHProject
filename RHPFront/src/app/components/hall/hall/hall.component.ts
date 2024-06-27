import { Component, Input, Signal, computed } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HallService } from '../../../services/hall.service';
import { IHall } from '../../../models/hall.model';

@Component({
  selector: 'app-hall',
  standalone: true,
  imports: [],
  templateUrl: './hall.component.html',
  styleUrl: './hall.component.css'
})
export class HallComponent {

  hall: Signal<IHall | null> = this.service.selectedHall;


  constructor(private route : ActivatedRoute, private service: HallService) { 
    this.route.params.subscribe(params => {
      let id = +params['id'];
      this.service.getHall(id);
    });

  
  }
}
