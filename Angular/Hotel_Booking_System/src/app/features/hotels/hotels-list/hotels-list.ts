import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Hotel } from '../../../shared/models/hotel';
import { HotelListService } from './hotel-list.service';
import { AsyncPipe } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faLocationDot } from '@fortawesome/free-solid-svg-icons';
import { RouterLink } from "@angular/router";




@Component({
  selector: 'app-hotels-list',
  imports: [AsyncPipe, FontAwesomeModule, RouterLink],
  templateUrl: './hotels-list.html',
  styleUrl: './hotels-list.scss',
})


export class HotelsList implements OnInit  {


  iconLocation = faLocationDot;

  

  public hotels$: Observable<Hotel[]>;

  constructor(private hotelService:HotelListService ){
    this.hotels$=hotelService.hotels$;
  }

  ngOnInit(): void {
    this.hotelService.loadHotels();
    this.hotels$.forEach(hotel=>console.log(hotel));
  }

 
}
