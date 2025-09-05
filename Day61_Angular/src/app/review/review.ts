import { Component } from '@angular/core';
import { IProduct } from '../product/ProductModel';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-review',
  imports: [FormsModule,CommonModule],
  templateUrl: './review.html',
  styleUrl: './review.css'
})
export class Review {
  product: IProduct={
    productName: "Pen",
    price:50,
    description: "Magic Pen",
    stock: 5,
    rating: 4,
    reviews: [
      'Very smooth writing experience!',
      'Ink dries fast, no smudges at all.',
      'Affordable and long-lasting.',
      'Perfect for exams and daily use.'
    ]
  };
}
