import { Injectable } from "@angular/core";
import { MasterService } from "../master/master.service";
import { ApiResponse } from "../../models/interfaces/Common/ApiResponse";
import { Observable } from "rxjs";

@Injectable({
	providedIn: "root",
})
export class GeminiService {
	endpoints = ["client/chatGemini"];
	private readonly sessionKey = "chatHistory";

	constructor(private master: MasterService) {}

	chat(chat: string): Observable<ApiResponse> {
		return this.master.post(this.endpoints[0], { promt: chat });
	}

	// Lấy dữ liệu lịch sử từ session
	getChatHistory(): string[] {
		const history = sessionStorage.getItem(this.sessionKey);
		return history ? JSON.parse(history) : [];
	}

	// Lưu lịch sử vào session
	saveChatHistory(messages: string[]): void {
		sessionStorage.setItem(this.sessionKey, JSON.stringify(messages));
	}

	// Xóa lịch sử
	clearChatHistory(): void {
		sessionStorage.removeItem(this.sessionKey);
	}
}
