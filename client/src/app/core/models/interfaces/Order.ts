export interface Order {
	id: number;
	total: number;
	status: boolean;
	hasInvoice: boolean;
	createdAt: Date;
	idTable: number;
	idStore: number;
}
