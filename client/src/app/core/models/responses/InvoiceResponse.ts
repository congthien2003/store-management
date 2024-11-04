import { Order } from "../interfaces/Order";
import { PaymentType } from "../interfaces/PaymentType";

export interface InvoiceResponse {
	id: number;
	createdAt: Date;
	finishedAt: Date;
	status: boolean;
	totalOrder: number;
	charge: number;
	total: number;
	order: Order;
	tableName: string;
	paymentType: PaymentType;
}
