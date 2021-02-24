import { ServiceSharedService } from './service-shared.service';
import { Injectable, Injector } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';


@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private _injector: Injector) {}

  intercept(req,next){
    let _serviceShared = this._injector.get(ServiceSharedService);
    let tokenReq = req.clone({
      setHeaders : {
        Authorization: `Bearer ${_serviceShared.getToken()}`
      }
    });
    return next.handle(tokenReq);
  }
}
