import { Component, signal } from '@angular/core';
import { ProductCard } from "./product-card/product-card";
import { StudentCard } from "./student-card/student-card";
import { CourseCard } from "./course-card/course-card";

@Component({
  selector: 'app-root',
  imports: [ProductCard, StudentCard, CourseCard],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Day59');
}
