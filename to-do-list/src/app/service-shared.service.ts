import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ListElement } from './list/list-interface.model';
import { Users } from './users/user-interface.model';

@Injectable({
  providedIn: 'root'
})
export class ServiceSharedService {
  readonly APIUrl = 'http://localhost:5000/api';

  constructor(private http: HttpClient) { }

  // tslint:disable-next-line: typedef-whitespace
  getLista():Observable<ListElement[]>{
    return this.http.get<ListElement[]>(this.APIUrl + '/ToDoList');
    
  }

  getUsers(): Observable<Users[]>{
    return this.http.get<Users[]>(this.APIUrl + '/ToDoList/Admin' );
  }

  // tslint:disable-next-line: typedef
  addLista(val: ListElement){
    return this.http.post(this.APIUrl + '/ToDoList', val);
  }

  login(user : Users){
    user.Roles = "Standard";
    return this.http.post(this.APIUrl + '/ToDoList/login', user);
  }

  getToken(){
    return localStorage.getItem('token');
  }

  // tslint:disable-next-line: typedef
  updateLista(val: ListElement){
    return this.http.put(this.APIUrl + '/TodoList', val);
  }

  // tslint:disable-next-line: typedef
  DeleteListaElement(val: any){
    // console.log(this.APIUrl + '/ToDoList/' + val);
    return this.http.delete(this.APIUrl + '/ToDoList/' + val);
  }
}
