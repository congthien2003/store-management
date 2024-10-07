import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatButtonModule } from '@angular/material/button';
import { MatCommonModule } from '@angular/material/core';
import { MatMenuModule } from '@angular/material/menu';
import { Router, RouterOutlet } from '@angular/router';
import { StoreService } from '../core/services/store.service';
import { AuthenticationService } from '../core/services/auth/authentication.service';
const MatModuleImport = [MatButtonModule, MatCommonModule, MatMenuModule];

@Component({
  selector: 'app-owner',
  standalone: true,
  imports: [CommonModule, MatModuleImport, RouterOutlet],
  templateUrl: './owner.component.html',
  styleUrls: ['./owner.component.scss'],
})
export class OwnerComponent {
  currentURL: string = 'dashboard';
  activeIndex: number = 0;
  id!: number;
  idUser!: number;
  idStore!: number;
  constructor(
    private router: Router,
    private service: StoreService,
    private authService: AuthenticationService
  ) {
    this.getIdStore();
  }
  getIdStore() {
    const idUser = this.authService.getIdUserFromToken();
    if (idUser) {
      this.service.getByIdUser(idUser).subscribe(
        (res) => {
          localStorage.setItem('idStore', res.data.id);
        },
        (err) => {
          console.error('Lỗi khi lấy id store', err);
        }
      );
    } else {
      console.error('Không có id user được tìm thấy trong session');
    }
  }
  swithRoute(index: number) {
    this.activeIndex = index;
    switch (index) {
      case 0: {
        this.router.navigate(['/owner/dashboard']);
        break;
      }
      case 1: {
        this.router.navigate(['/owner/my-store']);
        break;
      }
      case 2: {
        this.router.navigate(['/owner/category']);
        break;
      }
      case 3: {
        this.router.navigate(['/owner/food']);
        break;
      }
      case 4: {
        this.router.navigate(['/owner/table']);
        break;
      }
      case 5: {
        this.router.navigate(['/owner/order']);
        break;
      }
      case 6: {
        this.router.navigate(['/owner/invoice']);
        break;
      }
      case 7: {
        this.router.navigate(['/owner/staff']);
        break;
      }
      case 8: {
        this.router.navigate(['/owner/analytics']);
        break;
      }
    }
  }

  bindActiveMenu(url: string) {
    switch (url) {
      case '/pages': {
        this.activeIndex = 0;
        break;
      }
      case '/pages/create': {
        this.activeIndex = 1;
        break;
      }
      case '/pages/find': {
        this.activeIndex = 2;
        break;
      }
      default: {
        this.activeIndex = -1;
        break;
      }
    }
  }
}
