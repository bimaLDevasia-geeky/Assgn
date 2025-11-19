import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from "@angular/router";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faHotel, faBed, faUsers, faCalendarCheck, faChevronRight, IconDefinition } from '@fortawesome/free-solid-svg-icons';

interface SidebarOption {
  name: string;
  path: string;
  icon: IconDefinition;
}

@Component({
  selector: 'app-sidebar',
  imports: [RouterLink, RouterLinkActive, FontAwesomeModule],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.scss',
})
export class Sidebar {
  faChevronRight = faChevronRight;

  public option: SidebarOption[] = [
    {
      name: 'Hotels', path: 'hotel', icon: faHotel
    }, 
    {
      name: 'Room Types', path: 'roomtype', icon: faBed
    },
    {
      name: 'Users', path: 'user', icon: faUsers
    },
    {
      name: 'Bookings', path: 'booking', icon: faCalendarCheck
    }
  ];

}
