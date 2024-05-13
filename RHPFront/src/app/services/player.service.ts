import { Injectable } from '@angular/core';
import { IPlayer } from '../models/player.model';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { API_URL } from '../shared/constants';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  constructor(private http: HttpClient) { }

  async createUser(player: IPlayer): Promise<number> {
    let status: number = 0;
    const response$ =  this.http.post<any>(API_URL + '/player' , player, {observe: 'response'});

    try{
      const res: HttpResponse<any> = await firstValueFrom(response$);
      status = res.status;

    }catch(err: any){
      console.error(err);
      status = err.status;
    }

    return status;

  }

  requestJoinHall(id: number, loggedInUserId: number) {
    const request = this.http.post(API_URL + '/hall/' + id + '/request', {playerId: loggedInUserId}).subscribe({

      next: response => {
        console.log(response);
        return response;
      },
      error: err => {
        console.error(err);
      }
    }  
    );
  }
  
  joinHall(id: number) {
    const request = this.http.post(API_URL + '/hall/' + id + '/join', {}).subscribe({
        next: response => {
          console.log(response);
          return response;
        },
        error: err => {
          console.error(err);
        }
      }  
      );
  }

}
