import { Injectable, signal } from '@angular/core';
import { Contact, ContactStatus, IPlayerCreate, UserPlayer, UserStatus } from '../models/player.model';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { API_URL } from '../shared/constants';
import { catchError, firstValueFrom, of, switchMap } from 'rxjs';
import { IContact } from '../shared/components/contacts/contacts-sidebar/contacts-sidebar.component';

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
    // this.http
    //   .get<IContact[]>(API_URL + '/player/contacts')
    //   .pipe(
    //     //needed instead of map to be able to cancel the previous call in case of an update event
    //     switchMap((data: IContact[]) => {
    //       return of(data.map((contact) => new Contact(contact)));
    //     })
    //   )
    //   .subscribe({
    //     next: (data: Contact[]) => {
    //       this.contactList.set(data);
    //     },
    //     error: (err) => {
    //       console.error(err);
    //     },
    //   });

    // ? Having Blocked and Rejected contacts in the mock list will result in empty list tags that will have an impact in the disposition of the tags. 
    //TODO: Review repository query to ensure only Pending and Accepted contacts are passed to the client. 
    this.contactList.set(
      [
        {
          userId: 'user456',
          name: 'Bob Smith',
          email: 'bob.smith@example.com',
          loggedIn: false,
          status: ContactStatus.Pending,
          userStatus: UserStatus.Away,
          lastLogin: new Date('2023-10-25T14:30:00.000Z'),
        },
        {
          userId: 'user101',
          name: 'Diana Jones',
          email: 'diana.jones@example.com',
          loggedIn: false,
          status: ContactStatus.Pending,
          userStatus: UserStatus.Online,
          lastLogin: new Date('2023-10-23T22:00:00.000Z'),
        },
        {
          userId: 'user123',
          name: 'Alice Johnson',
          email: 'alice.johnson@example.com',
          loggedIn: true,
          status: ContactStatus.Accepted,
          userStatus: UserStatus.Online,
          lastLogin: new Date('2023-10-26T10:00:00.000Z'),
        },
        {
          userId: 'user112',
          name: 'Eric Davis',
          email: 'eric.davis@example.com',
          loggedIn: true,
          status: ContactStatus.Accepted,
          userStatus: UserStatus.Away,
          lastLogin: new Date('2023-10-22T02:45:00.000Z'),
        },
        {
        userId: 'user789',
        name: 'Charlie Brown',
        email: 'charlie.brown@example.com',
          loggedIn: true,
          status: ContactStatus.Accepted,
          userStatus: UserStatus.Offline,
          lastLogin: new Date('2023-10-24T18:15:00.000Z'),
        },
      ]);
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
