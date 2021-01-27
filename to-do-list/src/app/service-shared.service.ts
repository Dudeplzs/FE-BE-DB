import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ServiceSharedService {
  readonly APIUrl = 'http://localhost:5000/api';

  constructor(private http: HttpClient) { }

  // tslint:disable-next-line: typedef-whitespace
  getLista():Observable<any[]>{
    return this.http.get<any>(this.APIUrl + '/ToDoList');
  }

  // tslint:disable-next-line: typedef
  addLista(val: any){
    return this.http.post(this.APIUrl + '/ToDoList', val);
  }

  // tslint:disable-next-line: typedef
  updateLista(val: any){
    return this.http.put(this.APIUrl + '/TodoList', val);
  }

  // tslint:disable-next-line: typedef
  DeleteListaElement(val: any){
    return this.http.delete(this.APIUrl + '/ToDoList', val);
  }
}
