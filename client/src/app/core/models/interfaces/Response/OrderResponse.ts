import { Table } from "../Table";

export interface OrderResponse {
	id: number;
	total: number;
	status: boolean;
	hasInvoice: boolean;
	idInvoice: number;
	createdAt: Date;
	table: Table;
	idStore: number;
}
