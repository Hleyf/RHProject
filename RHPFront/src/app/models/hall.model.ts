import { Player } from "./player.model";

export interface IHall {
    id: number;
    title: string;
    description: string;
    players: Player[];
}
