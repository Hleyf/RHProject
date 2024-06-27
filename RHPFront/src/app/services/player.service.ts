import { Injectable, signal } from '@angular/core';
import { Contact, IPlayerCreate, UserPlayer } from '../models/player.model';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { API_URL } from '../shared/constants';
import { catchError, firstValueFrom, map, of, switchMap } from 'rxjs';
import { IContact } from '../shared/components/contacts/contact-list.component';

@Injectable({
  providedIn: 'root',
})
export class PlayerService {
  contactList = signal<Contact[]>([]);
  player = signal<UserPlayer | null>(null);

  constructor(private http: HttpClient) {}

  async createUser(player: IPlayerCreate): Promise<number> {
    let status: number = 0;
    const response$ = this.http.post<any>(API_URL + '/player', player, {
      observe: 'response',
    });

    try {
      const res: HttpResponse<any> = await firstValueFrom(response$);
      status = res.status;
    } catch (err: any) {
      console.error(err);
      status = err.status;
    }

    return status;
  }

  requestJoinHall(id: number, loggedInUserId: number) {
    const request = this.http
      .post(API_URL + '/hall/' + id + '/request', { playerId: loggedInUserId })
      .subscribe({
        next: (response) => {
          console.log(response);
          return response;
        },
        error: (err) => {
          console.error(err);
        },
      });
  }

  joinHall(id: number) {
    const request = this.http
      .post(API_URL + '/hall/' + id + '/join', {})
      .subscribe({
        next: (response) => {
          console.log(response);
          return response;
        },
        error: (err) => {
          console.error(err);
        },
      });
  }

  async getLoggedPlayer() {
    const request: UserPlayer = await firstValueFrom(
      this.http.get<UserPlayer>(API_URL + '/player').pipe(
        catchError((error) => {
          console.error(error);
          return [];
        })
      )
    );
    this.player.set(request);
  }

  getPlayerContacts() {
    this.http
      .get<IContact[]>(API_URL + '/player/contacts')
      .pipe(
        switchMap((data: IContact[]) => {
          //needed instead of map to be able to cancel the previous call in case of an update event
          return of(data.map((contact) => new Contact(contact)));
        })
      )
      .subscribe({
        next: (data: Contact[]) => {
          this.contactList.set(data);
        },
        error: (err) => {
          console.error(err);
        },
      });
  }

  searchContacts(searchTerm: string) {
    this.http
      .get<IContact[]>(API_URL + '/player/contacts/search/' + searchTerm)
      .pipe(
        switchMap((data: IContact[]) => {
          return of(data.map((contact) => new Contact(contact)));
        })
      )
      .subscribe({
        next: (data: Contact[]) => {
          this.contactList.set(data);
        },
        error: (err) => {
          console.error(err);
        },
      });
  }

  requestContact(id: number) {
    this.http
      .post(API_URL + '/player/contacts/request/' + id, {})
      .subscribe({
        next: (response) => {
          console.log(response);
          return response;
        },
        error: (err) => {
          console.error(err);
        },
      });
  }
  deleteContact(id: number) {
    this.http
      .delete(API_URL + '/player/contacts/' + id)
      .subscribe({
        next: (response) => {
          console.log(response);
          return response;
        },
        error: (err) => {
          console.error(err);
        },
      });
  }
}
