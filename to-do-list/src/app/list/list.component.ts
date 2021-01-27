import { ServiceSharedService } from './../service-shared.service';
import { LogService } from './../logg.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  // lista: ListElement [] = [];
  // checkBoxStatus = false;
  listaElement: any = [];

  constructor(private logservice: LogService,
              private serviceShare: ServiceSharedService) { }

  // tslint:disable-next-line: typedef
  ngOnInit(): void {
    this.refreshLista();
  }

  // tslint:disable-next-line: typedef
  refreshLista(){
    this.serviceShare.getLista().subscribe(
      data => { this.listaElement = data;
      });
  }

  // tslint:disable-next-line: typedef
  // onGo(nameInput: HTMLInputElement){
  //   const listElement: ListElement = {text: nameInput.value, status: false};
  //   this.lista.push(listElement);
  //   console.log(this.lista);
  // }

  // tslint:disable-next-line: typedef
  // onClear(){
  //   this.listaElement = [];
  // }

  // tslint:disable-next-line: typedef
  // onRemoveItem(index: number){
  //   this.lista.splice(index, 1);
  // }

  // tslint:disable-next-line: typedef
  onCheckboxClick(element: any){
    element.Estado = !element.Estado;
    // var var = {}
    this.serviceShare.updateLista(element);
    // this.lista[index];
    // this.logservice.logChange(element.text, element.status);
    console.log();
  }


}

// export interface ListElement{
//   id: number;
//   text: string;
//   status: boolean;
// }
