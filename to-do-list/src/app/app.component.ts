import { Component } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  selectorStored = 'toDoList';

  onNavBarSelect(selectorChoose: string){
    this.selectorStored = selectorChoose;
  }
}
