import { Users } from './../users/user-interface.model';
import { Component, OnInit } from '@angular/core';
import { ServiceSharedService } from '../service-shared.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  users: Users [] = [];

  constructor(private _serviceShare: ServiceSharedService,
              private route: Router) { }

  ngOnInit(): void {
  }

  loginUser(email: HTMLInputElement, username: HTMLInputElement, password: HTMLInputElement )
  {
    const _user: Users = {id: 0, Email: email.value, Username: username.value, Password: password.value, Roles: 'Standard' };
    this._serviceShare.login(_user).subscribe(
      (res: any)  => {
        localStorage.setItem('token', res.token);
        // this.route.navigate(['/users']);
      },
      err => console.log(err)
    );
  }
}
