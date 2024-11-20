import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AuthenticationService } from "src/app/core/services/auth/authentication.service";
import { UserService } from "src/app/core/services/user/user.service";
import { User } from "src/app/core/models/interfaces/User"; 
import { FormGroup, FormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';

import { ToastrService } from "ngx-toastr";

@Component({
  selector: 'app-infor-user',
  standalone: true,
  imports: [
    CommonModule, 
    FormsModule, 
    MatButtonModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
  ],
  templateUrl: './infor-user.component.html',
  styleUrls: ['./infor-user.component.scss']
})
export class InforUserComponent implements OnInit {
  idUser: number = 0; // Current user's ID
  user!: User; // User information
  isEditing: boolean = false; // Toggle edit mode
  validateForm!: FormGroup;

	constructor(
		private authService: AuthenticationService,
		private userService: UserService,
    private toastr: ToastrService
	) {}

	ngOnInit(): void {
    this.idUser = this.authService.getIdFromToken();
    this.loadUser();
	}
  loadUser() {
    this.userService.getById(this.idUser).subscribe({
      next: (response) => {
        this.user = response.data; // Adjust based on your API response structure
      },
      error: (err) => {
        console.error("Error fetching user:", err);
      },
    });
  }

  toggleEdit() {
    if (this.isEditing) {
      this.saveChanges();
    }
    this.isEditing = !this.isEditing;
  }
  saveChanges() {
    this.userService.update(this.user).subscribe({
      next: (res) => {
				if (res.isSuccess) {
					this.toastr.success(res.message, "Thành công", {
						timeOut: 3000,
					});
				} else {
					this.toastr.error(res.message, "Thất bại", {
						timeOut: 3000,
					});
				}
			},
    });
  }
}
