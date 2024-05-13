
export interface IPlayer {
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
