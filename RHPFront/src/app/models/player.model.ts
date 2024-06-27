
export interface IPlayerCreate {
  id?: number;
  name: string;
  email: string;
  password?: string;
}

export class Player {
  id?: number;
  name: string;
  email: string;
  password?: string;
 
  constructor(player: any) {
    this.id = player.id;
    this.name = player.name;
    this.email = player.email;
    this.password = player.password;
  }
}

export class Contact {
  userId: string;
  name: string;
  email: string;
  loggedIn: boolean;
  status: string;
  lastLogin: Date;

  constructor(contact: any){
    this.userId = contact.userId;
    this.name = contact.name;
    this.email = contact.email;
    this.loggedIn = contact.loggedIn;
    this.status = contact.status;
    this.lastLogin = contact.lastLogin;

  }
}

export class UserPlayer {
  id: number;
  userId: number;
  status: string;
  name: string;
  email: string;

  constructor(player: any) {
    this.id = player.id;
    this.userId = player.userId;
    this.status = player.status;
    this.name = player.name;
    this.email = player.email;
  }
}
