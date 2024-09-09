import { ApiResponse } from "./ApiResponse";
import { Pagination } from "./Pagination";

export interface ApiResponsePagi {
	data: Pagination;
	success: boolean;
	message: string;
}
