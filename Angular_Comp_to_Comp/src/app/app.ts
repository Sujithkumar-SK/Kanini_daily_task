import { Component, signal } from '@angular/core';
import { Child } from "./child/child";
import { User } from './child/User.Model';

@Component({
  selector: 'app-root',
  imports: [Child],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Comp_to_Comp');
  show = "hello we are done";

  user : User ={
    name: "sujith kumar s",
    age : 22,
    gender: "male"
  }

  OnClick(e:User){
    console.log(e);
  }
}
