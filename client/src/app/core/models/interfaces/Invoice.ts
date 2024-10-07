export interface Invoice {
  id: number;
  createdAt: Date;
  finishedAt: Date;
  status: boolean;
  totalOrder: number;
  idOrder: number;
  idPaymentType: number;
}
