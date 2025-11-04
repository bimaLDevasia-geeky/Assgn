import { Component, signal,input } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TryingOut } from './components/trying-out/trying-out';

@Component({
  selector: 'app-root',
  imports: [TryingOut],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal("hello from parent");

  messageFromChild = signal<string>("afa");

  receiveMessage = (message: string) => {
    this.messageFromChild.set(message);
  }
  
}
