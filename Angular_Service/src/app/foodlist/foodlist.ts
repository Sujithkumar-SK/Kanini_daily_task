import { Component, inject } from '@angular/core';
import { Food } from '../food';
import { IFood } from '../Models/Food.model';

@Component({
  selector: 'app-foodlist',
  imports: [],
  templateUrl: './foodlist.html',
  styleUrl: './foodlist.css'
})
export class Foodlist {
  foodservice = inject(Food);

  viewDetailsFood(food: IFood){
    this.foodservice.onFoodSelected(food);
  }

  addFood(food:IFood){
    this.foodservice.addToCart(food);
  }
}
