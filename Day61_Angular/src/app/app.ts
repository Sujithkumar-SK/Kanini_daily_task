import { Component, signal } from '@angular/core';
import { Product } from "./product/product";
import { User } from "./user/user";
import { Review } from "./review/review";

@Component({
  selector: 'app-root',
  imports: [Product, User, Review],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Day61');
}
