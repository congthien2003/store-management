import { Injectable } from "@angular/core";
import { createClient } from "@supabase/supabase-js";
import { environment } from "src/environments/environment.development";

@Injectable({
	providedIn: "root",
})
export class SupabaseService {
	// Create Supabase client
	supabase = createClient(
		environment.supabaseConfig.Url,
		environment.supabaseConfig.apiKey
	);
	constructor() {
		console.log(this.supabase);
	}

	downLoadImage(path: string) {
		return this.supabase.storage.from("avatars").download(path);
	}

	uploadAvatar(filePath: string, file: File) {
		return this.supabase.storage.from("avatars").upload(filePath, file);
	}
}
