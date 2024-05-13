import { Component, Inject, OnInit, Signal, computed, effect, signal } from '@angular/core';
import { MODAL_DATA, ModalService } from '../../../../services/modal.service';
import { HallService } from '../../../../services/hall.service';
import { Hall } from '../../../../models/hall.model';
import { AuthService } from '../../../auth/auth.service';
import { PlayerService } from '../../../../services/player.service';
import { BehaviorSubject } from 'rxjs';
import { CommonModule } from '@angular/common';

export interface IHallPreview {
  id: number;
  name: string;
  description: string;

}

@Component({
  selector: 'app-hall-preview',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './hall-preview.component.html',
  styleUrl: './hall-preview.component.css'
})
export class HallPreviewComponent implements OnInit{

  isLoading: boolean = true;
  readonly hall : Signal<Hall | null> = computed(() => this.hallService.selectedHall());
  loggedInUserId: number ;
  userInHall$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  constructor(
    @Inject(MODAL_DATA) public data: any,
    private modalService: ModalService, 
    private hallService: HallService,
    private authService: AuthService,
    private playerService: PlayerService
    ) {
      effect(() => {
        this.isLoading = this.hallService.isHallLoading();
      });

      effect(() => {
        const hall = this.hall();
        if(hall?.id) {
          this.updateUserInHall(hall.players.some(p => p.id === this.loggedInUserId));
        }
      });

    this.loggedInUserId = Number(this.authService.getLoggedInUser());
    this.hallService.getHall(data.id);
  }
  updateUserInHall(value: boolean) {
    this.userInHall$.next(value);
  }

  ngOnInit(): void {
  }


  joinHall() {
    this.playerService.joinHall(this.hall()!.id);
    }

  requestJoinHall() {
    this.playerService.requestJoinHall(this.hall()!.id, this.loggedInUserId);
  }

  onClose() : void {
    this.modalService.close({id: 55, name: '', description: ''});
  }
}
function allowSignalWrites(arg0: () => void) {
  throw new Error('Function not implemented.');
}

