import { Component } from '@angular/core';
import { RouterLinkActive, RouterLinkWithHref } from '@angular/router';

@Component({
  selector: 'app-navigation',
  imports: [ RouterLinkActive, RouterLinkWithHref],
  templateUrl: './navigation.html',
  styleUrl: './navigation.scss',
})
export class Navigation {

}
