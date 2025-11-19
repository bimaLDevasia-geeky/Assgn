import { Component } from '@angular/core';
import { Sidebar } from './pages/sidebar/sidebar';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './components/navbar/navbar';

@Component({
  selector: 'app-admin-dashboard',
  imports: [Sidebar, RouterOutlet, NavbarComponent],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.scss',
})
export class AdminDashboard {

}
