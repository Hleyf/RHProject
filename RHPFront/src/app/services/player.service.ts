import { Injectable, signal } from '@angular/core';
import { IPlayer } from '../models/player.model';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { API_URL } from '../shared/constants';
import { catchError, firstValueFrom } from 'rxjs';
import { IContact } from '../components/contacts/contact-list/contact-list.component';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  contactList = signal<IContact[]>([]);

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

  getPlayerContacts() {
    const request = this.http.get<IContact[]>(API_URL + '/player/contacts')
    .pipe(catchError((error) => {
      console.error(error);
      return [];
    }))
    .subscribe((data: IContact[]) => {
      this.contactList.set(data);
    });
  }

  searchContacts(searchTerm: string) {
    const request = this.http.get<IContact[]>(API_URL + '/player/contacts/search/' + searchTerm)
    .pipe(catchError((error) => {
      console.error(error);
      return [];
    }))
    .subscribe((data: IContact[]) => {
      this.contactList.set(data);
    });
  }

  requestContact(id: number) {
    const request = this.http.post(API_URL + '/player/contacts/request/' + id, {}).subscribe({
      next: response => {
        console.log(response);
        return response;
      },
      error: err => {
        console.error(err);
      }
    });
  }
    deleteContact(id: number) {
      const request = this.http.delete(API_URL + '/player/contacts/' + id).subscribe({
        next: response => {
          console.log(response);
          return response;
        },
        error: err => {
          console.error(err);
        }
      });
    }
  }

