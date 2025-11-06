import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-hotels-details',
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './hotels-details.html',
  styleUrl: './hotels-details.scss',
})
export class HotelsDetails {

}
