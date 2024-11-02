import { Table } from "../interfaces/Table";

export interface OrderResponse {
	id: number;
	total: number;
	status: boolean;
	createdAt: Date;
	table: Table;
	idStore: number;
}
