
export interface IPlayer {
  id?: number;
  name: string;
  email: string;
  password: string;
}

export class Player {
  name: string;
  email: string;
  password: string;
 
  constructor(user: any) {
    this.name = user.name;
    this.email = user.email;
    this.password = user.password;
  }

}
