import { Component, OnInit } from '@angular/core';
import { ServiceSharedService } from '../service-shared.service';
import { Users } from './user-interface.model';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  users: Users [] = [];

  constructor(private serviceShare: ServiceSharedService) { }

  ngOnInit(){
    this.refreshUsers();
  }

  refreshUsers(){
    this.serviceShare.getUsers().subscribe(
      data => { this.users = data});
  }


}
