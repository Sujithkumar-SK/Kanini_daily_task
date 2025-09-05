import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Product } from '../product';
import { CommonModule } from '@angular/common';



@Component({
  selector: 'app-sample',
  imports: [FormsModule, CommonModule],
  templateUrl: './sample.html',
  styleUrl: './sample.css'
})
export class Sample {
  //interpoplation binding
  fname:string = "sujith";
  lname:string = "Kumar";
  //property binding
  imgname:string = "devops.jpg";

  //Event binding
  showPassword: boolean = false;

  TogglePassword(){
    this.showPassword = !this.showPassword;
  }
  //Two way binding
  txtTesting: string="";
  category:string[]="";

  num1:number = 0;
  num2:number=0;
  productList: Product[]=[
    {productId:1,productName:"Pen",productImage:"pen.png",category:"student"},
    {productId:2,productName:"Book",productImage:"book.png",category:"student"},
    {productId:3,productName:"Tool",productImage:"tool.png",category:"worker"},
  ]

  showProduct: boolean = false;
  toggleProduct(){
    this.showProduct=!this.showProduct
  }
}
