import { Component } from '@angular/core';
import { IUser } from './usermodel';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user',
  imports: [FormsModule],
  templateUrl: './user.html',
  styleUrl: './user.css'
})
export class User {
  user:IUser={
    name: "Sujith Kumar S",
    email: "sujith@gmai.com"
  };
}
