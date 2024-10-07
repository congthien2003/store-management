export interface Order {
  id: number;
  nameUser: string;
  phoneUser: string;
  status: boolean;
  total: number;
  createdAt: Date;
  idTable: number;
}
