import { Store } from "./Store";

export interface User {
	id: number;
	username: string;
	email: string;
	password: string;
	phones: string;
	role: number;
	store?: Store | null;
}
