import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, RouterOutlet, MatButtonModule],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss'],
})
export class AdminComponent {
  currentURL: string = 'dashboard';
  activeIndex: number = 0;

  constructor(private router: Router) {}

  swithRoute(index: number) {
    this.activeIndex = index;
    switch (index) {
      case 0: {
        this.router.navigate(['/admin/dashboard']);
        break;
      }
      case 1: {
        this.router.navigate(['/admin/user']);
        break;
      }
      case 2: {
        this.router.navigate(['/admin/owner']);
        break;
      }
      case 3: {
        this.router.navigate(['/admin/store']);
        break;
      }
      case 4: {
        this.router.navigate(['/admin/ticket']);
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

  logOut(): void {
    this.router.navigate(['/auth/login']);
    localStorage.clear();
  }
}
