import { secretKeyToken } from "../constant/secret";
export class JwtManager {
	encrypt(plainText: string): string {
		let newString: string = "";
		for (let i = 0; i < plainText.length; i++) {
			newString += plainText.charCodeAt(i) + 3;
		}
		return newString;
	}

	decrypt(plainText: string): string {
		let newString: string = "";
		for (let i = 0; i < plainText.length; i++) {
			newString += plainText.charCodeAt(i) - 3;
		}
		return newString;
	}

	encryptToken(token: string): string {
		return this.encrypt(token);
	}

	decryptToken(token: string): string {
		return this.decrypt(token);
	}

	getToken(): string {
		const token = localStorage.getItem("token");
		return token ? this.decryptToken(token) : "";
	}

	setToken(token: string): void {
		const tokenEncrypt = this.encryptToken(token);
		localStorage.setItem("token", tokenEncrypt);
	}
}
