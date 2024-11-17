import { Injectable } from "@angular/core";
import { AngularFireStorage } from "@angular/fire/compat/storage";
import { Observable, finalize } from "rxjs";
import { NgxImageCompressService, UploadResponse } from "ngx-image-compress";
import { Store } from "../models/interfaces/Store";
import { getStorage, ref, uploadBytes } from "firebase/storage";
@Injectable({
	providedIn: "root",
})
export class FirebaseService {
	fileUpload!: File;
	compressedImage!: string;
	store!: Store;
	constructor(
		private storage: AngularFireStorage,
		private imageCompress: NgxImageCompressService
	) {
		this.store = JSON.parse(sessionStorage.getItem("storeInfo") ?? "");
	}

	// Hàm convert base64 to file
	convertBase64ToFile(base64Image: string, fileName: string): File {
		const arr = base64Image.split(",");
		const mime = arr[0].match(/:(.*?);/)![1];
		const bstr = atob(arr[1]);
		let n = bstr.length;
		const u8arr = new Uint8Array(n);

		while (n--) {
			u8arr[n] = bstr.charCodeAt(n);
		}

		return new File([u8arr], fileName, { type: mime });
	}

	// Hàm upload file lên Firebase
	uploadFileImage(file: File): Observable<string> {
		return new Observable<string>((observer) => {
			const filePath = `imagesClient/store_${
				this.store.id
			}/foods/${Date.now()}_${file.name}`; // Tạo đường dẫn cho file
			const fileRef = this.storage.ref(filePath);
			const task = this.storage.upload(filePath, file);
			task.snapshotChanges()
				.pipe(
					finalize(() => {
						fileRef.getDownloadURL().subscribe((downloadURL) => {
							observer.next(downloadURL); // Trả về URL tải file
							observer.complete();
						});
					})
				)
				.subscribe();
		});
	}

	uploadFileImageBase64(fileName: string, data: string): Observable<string> {
		return new Observable<string>((observer) => {
			const filePath = `imagesClient/store_${
				this.store.id
			}/foods/${Date.now()}_${fileName}`; // Tạo đường dẫn cho file
			const fileRef = this.storage.ref(filePath);
			const task = this.storage.upload(filePath, data);
			task.snapshotChanges()
				.pipe(
					finalize(() => {
						fileRef.getDownloadURL().subscribe((downloadURL) => {
							observer.next(downloadURL); // Trả về URL tải file
							observer.complete();
						});
					})
				)
				.subscribe();
		});
	}

	async deleteFileFromFirebase(imageUrl: string): Promise<void> {
		const storageRef = this.storage.refFromURL(imageUrl); // Lấy tham chiếu đến file dựa trên URL
		return storageRef.delete().toPromise(); // Xóa file từ Firebase Storage
	}
}
