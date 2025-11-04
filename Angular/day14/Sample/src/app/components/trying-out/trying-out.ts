import { Component, input, Input, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-trying-out',
  imports: [FormsModule],
  templateUrl: './trying-out.html',
  styleUrl: './trying-out.scss',
 
})
export class TryingOut implements OnInit {


   year: number=2024 ;

  getTitle = input('Bimal Devasia');

  messageToParent = input<(message: string) => void>();

sendMessageToParent = () => {
  this.messageToParent()?.('Hello from hbajdb');

}
ngOnInit(): void {
  this.sendMessageToParent();
}


alertme():void{
  alert('Hello there');
}

checkLeapYear(year:number ):void{
    if ((year % 4 === 0 && year % 100 !== 0) || (year % 400 === 0)) {
        console.log(year + " is a leap year.");
    } else {
        console.log(year + " is not a leap year.");
    }  
} 

}