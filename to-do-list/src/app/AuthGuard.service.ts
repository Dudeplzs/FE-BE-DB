import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable()
export class AuthGuard implements CanActivate{

    constructor(private _authService: AuthService,
                private _router: Router) {}

canActivate(route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean> | Promise <boolean> | boolean {
return this._authService.isAuthenticated().then(
    (authenticated: boolean) => {
        if (authenticated) {
            return true;
        } else  {
            // tslint:disable-next-line: no-unused-expression
            this._router.navigate(['/login']);
            return false;
        }
    }
);
}
}