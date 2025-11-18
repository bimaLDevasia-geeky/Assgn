import { Component, inject, signal, ChangeDetectionStrategy, HostListener, DestroyRef } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../../../../core/auth/services/auth.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faSignOutAlt, faUser, faBars, faHotel, faChevronDown } from '@fortawesome/free-solid-svg-icons';
import { CommonModule } from '@angular/common';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-navbar',
  imports: [CommonModule, FontAwesomeModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  host: {
    '(document:click)': 'onDocumentClick($event)'
  }
})
export class NavbarComponent {
  private authService = inject(AuthService);
  private router = inject(Router);

  faSignOutAlt = faSignOutAlt;
  faUser = faUser;
  faBars = faBars;
  faHotel = faHotel;
  faChevronDown = faChevronDown;

  private ref = inject(DestroyRef);
  userName = signal<string>('Admin User');
  showDropdown = signal<boolean>(false);
  usermail = signal<string | null>(null);

    constructor(private authservice : AuthService) {
    const user = this.authservice.currentUser$.pipe(
        takeUntilDestroyed(this.ref)
    ).subscribe(user => {
      if (user) {
        this.usermail.set(user.email);
      }
    });
  }

  toggleDropdown(): void {
    this.showDropdown.set(!this.showDropdown());
  }

  onDocumentClick(event: MouseEvent): void {
    const target = event.target as HTMLElement;
    if (!target.closest('.navbar-user')) {
      this.showDropdown.set(false);
    }
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
