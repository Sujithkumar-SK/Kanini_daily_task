import { Component } from '@angular/core';

@Component({
  selector: 'app-course-card',
  imports: [],
  templateUrl: './course-card.html',
  styleUrl: './course-card.css'
})
export class CourseCard {
  courseName: string = "FullStack"
  duration: string = "8 Months"
  trainerName: string = "Mam"
}
