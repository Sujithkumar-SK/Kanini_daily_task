import { Component } from '@angular/core';

@Component({
  selector: 'app-product-card',
  imports: [],
  templateUrl: './product-card.html',
  styleUrl: './product-card.css'
})
export class ProductCard {
  productName: string = "EarPod"
  price: number = 2000
  inStock: boolean = true
  imageUrl: string = "earpod.png"
  inActive: boolean = false
}
