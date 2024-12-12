import { Food } from "./Food";

export interface Combo {
	id: number;
	name: string;
	description: string;
	foods: Food[];
	price: number;
}
