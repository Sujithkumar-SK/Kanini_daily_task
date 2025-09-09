import { Component, computed, EventEmitter, Input, Output, signal } from '@angular/core';
import { User } from './User.Model';

@Component({
  selector: 'app-child',
  imports: [],
  templateUrl: './child.html',
  styleUrl: './child.css'
})
export class Child {
  @Input() display:string ="hi done...";

    @Input() childUser:User= {
    name : "",
    age: 0,
    gender:""
  }

  @Output() childEvent : EventEmitter<User> = new EventEmitter();
  
  OnSubmit(){
    if (this.childEvent){
      this.childUser.name="testing";
    }
    this.childEvent.emit(this.childUser);
  }

  count = signal(10);
  Increment(){
    this.count.update(i=>i+1);
  }
  Decrement(){
    this.count.update(i=>i-1);
  }
  color = computed(()=>{
    if(this.count()>3) return 'green';
    if(this.count()<0) return 'red';
    return 'black';
  })
}
