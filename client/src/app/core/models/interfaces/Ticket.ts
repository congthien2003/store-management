import { User } from "./User";
export interface Ticket {
	id: number;
	title: string;
	description: string;
	status: number;
	createdAt: Date;
	requestBy: User;
}
