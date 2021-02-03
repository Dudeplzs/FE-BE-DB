import { ServiceSharedService } from './../service-shared.service';
import { LogService } from './../logg.service';
import { Component, OnInit } from '@angular/core';
import { ListElement } from './list-interface.model';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  lista: ListElement [] = [];
  elementEstado = false;
  // listaElement: any = [];
  


  constructor(private logservice: LogService,
              private serviceShare: ServiceSharedService) { }

  // tslint:disable-next-line: typedef
  ngOnInit(): void {
    this.refreshLista();
  }

  // tslint:disable-next-line: typedef
  refreshLista(){
    this.serviceShare.getLista().subscribe(
      data => { this.lista = data});
      // window.location.reload();
    // console.log(this.lista);
  }

  // tslint:disable-next-line: typedef
  onAdd(nameInput: HTMLInputElement){
    const listElement: ListElement = {id: 0, Nome: nameInput.value, Estado: false};
    this.serviceShare.addLista(listElement).subscribe(
      res=>{alert(res.toString());
      this.ngOnInit();
    });
   
    // window.location.reload();
    // console.log(listElement);
  }

  // tslint:disable-next-line: typedef
  // onClear(){
  //   this.listaElement = [];
  // }

  // tslint:disable-next-line: typedef
  onRemoveItem(elementId: number){
    this.serviceShare.DeleteListaElement(elementId).subscribe(
      data=>{alert(data.toString());
        this.ngOnInit();
    });
    // window.location.reload();
  }

  // tslint:disable-next-line: typedef
  onStateChange(element: ListElement){
    element.Estado = !element.Estado;
    this.serviceShare.updateLista(element).subscribe(res=>{
      alert(res.toString());
    });
    // console.log(element);
  }


}

