import { Food } from "../interfaces/Food";

export interface OrderDetailResponse {
	quantity: number;
	food: Food;
	total: number;
	statusProcess: number;
}
