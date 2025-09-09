import { Injectable, signal } from '@angular/core';
import { IFood } from './Models/Food.model';

@Injectable({
  providedIn: 'root'
})
export class Food {
  foods = signal<IFood[]>([
    { id: 1, name: "Biriyani", price: 250, category: "Meal", imageUrl: "/FoodImages/Biriyani.png" },
    { id: 2, name: "Dosa", price: 100, category: "BreakFast", imageUrl: "/FoodImages/Dosa.png" },
    { id: 3, name: "Poori", price: 60, category: "Dinner", imageUrl: "/FoodImages/Poori.png" }
  ])

  selectedfood = signal<IFood | null>(null);
  cart = signal<IFood[]>([]);

  onFoodSelected(food:IFood){
    this.selectedfood.set(food);
  }
  addToCart(food:IFood){
    this.cart.update(i=>[...i,food]);
  }
  clearCart(){
    this.cart.set([]);
  }
  removeFromCart(index: number){
    this.cart.update(i=>i.filter((_,i)=>i != index));
  }
}
