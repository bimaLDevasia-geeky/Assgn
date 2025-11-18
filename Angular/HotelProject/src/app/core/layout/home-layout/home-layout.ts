import { Component, inject, signal } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faHotel, faUser, faSignOutAlt, faUserCircle } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../../auth/services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home-layout',
  imports: [RouterOutlet, FontAwesomeModule, RouterLink, CommonModule],
  templateUrl: './home-layout.html',
  styleUrl: './home-layout.scss',
})
export class HomeLayout {
  private authService = inject(AuthService);
  private router = inject(Router);

  hotelicon = faHotel;
  faUser = faUser;
  faSignOutAlt = faSignOutAlt;
  faUserCircle = faUserCircle;

  currentUser = this.authService.currentUser$;
  showDropdown = signal<boolean>(false);

  toggleDropdown(): void {
    this.showDropdown.set(!this.showDropdown());
  }

  closeDropdown(): void {
    this.showDropdown.set(false);
  }

  logout(): void {
    this.authService.logout();
    this.showDropdown.set(false);
    this.router.navigate(['/login']);
  }

  onDocumentClick(event: MouseEvent): void {
    const target = event.target as HTMLElement;
    if (!target.closest('.user-menu')) {
      this.showDropdown.set(false);
    }
  }
}

