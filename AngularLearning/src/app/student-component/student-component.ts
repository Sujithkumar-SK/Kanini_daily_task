import { CommonModule } from '@angular/common';
import { Component, computed, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-student-component',
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './student-component.html',
  styleUrl: './student-component.css'
})
export class StudentComponent {
  students = signal<{name:string;age:number}[]>([]);
  totalstd = computed(()=>this.students.length);
  form!: FormGroup;
  ngOnInit(){
    this.form = this.fb.group({
    name:['',Validators.required],
    age:['',Validators.required]
  });
}
  constructor (private fb:FormBuilder){}
  addStudent(){
    if (this.form.valid) {
      this.students.update(stds => [...stds, this.form.value as any]);
      this.form.reset();
    }
  }
  deleteStudent(index: number){
    this.students.update(std=>std.filter((_,i)=>i!==index))
  }
  clearStudents(){
    this.students.set([]);
  }
}