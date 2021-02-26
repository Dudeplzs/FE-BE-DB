import { ServiceSharedService } from './service-shared.service';
import { Injectable, Injector } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { AuthService } from './auth.service';


@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private _injector: Injector) {}

  intercept(req,next){
    let _authService = this._injector.get(AuthService);
    let tokenReq = req.clone({
      setHeaders : {
        Authorization: `Bearer ${_authService.getToken()}`
      }
    });
    return next.handle(tokenReq);
  }
}
