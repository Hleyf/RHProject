import { Player } from "./player.model";

export interface IHall {
    id: number;
    title: string;
    description: string;
    players: Player[];
}

export class Hall implements IHall {
    id: number;
    title: string;
    description: string;
    players: Player[];

    constructor(data: IHall) {
        this.id = data.id;
        this.title = data.title;
        this.description = data.description;
        this.players = data.players;
    }
}
