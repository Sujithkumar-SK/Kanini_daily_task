import { Component, PendingTasks } from '@angular/core';
import { IProduct } from './ProductModel';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-product',
  imports: [FormsModule,CommonModule],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class Product {
  product: IProduct={
    productName: "Pen",
    price: 50,
    description: "Magic Pen",
    stock :3,
    rating:3
  };
  cart: IProduct[] = [];
  addToCart(added_data:IProduct){
    this.cart.push(added_data);
  }
  getRatingColor(): string{
    if(this.product.rating>=4){
      return'green';
    }else if(this.product.rating>=2){
      return 'orange';
    }else{
      return 'red';
    }
  }
}
