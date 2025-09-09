import { Component, computed, inject } from '@angular/core';
import { Food } from '../food';
import { IFood } from '../Models/Food.model';

@Component({
  selector: 'app-fooddetail',
  imports: [],
  templateUrl: './fooddetail.html',
  styleUrl: './fooddetail.css'
})
export class Fooddetail {
  foodservice = inject(Food);
  seletedFood = computed(()=>this.foodservice.selectedfood());
}
