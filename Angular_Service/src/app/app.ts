import { Component, signal } from '@angular/core';
import { Foodlist } from "./foodlist/foodlist";
import { Fooddetail } from "./fooddetail/fooddetail";
import { Foodcart } from "./foodcart/foodcart";

@Component({
  selector: 'app-root',
  imports: [Foodlist, Fooddetail, Foodcart],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Angular_Services');
}
