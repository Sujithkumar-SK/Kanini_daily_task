import { Component } from '@angular/core';

@Component({
  selector: 'app-student-card',
  imports: [],
  templateUrl: './student-card.html',
  styleUrl: './student-card.css'
})
export class StudentCard {
  studentName: string = "Sujith Kumar S"
  rollNumber: string = "201EC264"
  Course: string = "Devops"
  profilePic : string = "profile.jpg"
}
