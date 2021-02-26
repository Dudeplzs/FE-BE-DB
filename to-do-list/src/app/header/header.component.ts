import { AuthService } from './../auth.service';
import { ServiceSharedService } from './../service-shared.service';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  @Output() selectorEmitter = new EventEmitter<string>();
  isUserLogged =  false;

  constructor(private _serviceShared: ServiceSharedService,
              private _router: Router,
              private _authService: AuthService) { }

  ngOnInit() {
  }

  onLogout(){
   this._router.navigate(['/login']);
   return this._authService.logout();
  }
  
  onLogin(){
    return this._authService.login();
  }
}
