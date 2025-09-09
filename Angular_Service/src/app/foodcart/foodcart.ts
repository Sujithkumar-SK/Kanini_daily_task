import { Component, computed, inject } from '@angular/core';
import { Food } from '../food';

@Component({
  selector: 'app-foodcart',
  imports: [],
  templateUrl: './foodcart.html',
  styleUrl: './foodcart.css'
})
export class Foodcart {
  foodService = inject(Food);

  cartItems = computed(()=>this.foodService.cart());

  total = computed(()=> this.foodService.cart().reduce((sum,food)=>sum+food.price,0));
}
