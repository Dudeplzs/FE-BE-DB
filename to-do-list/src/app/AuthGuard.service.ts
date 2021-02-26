// import { Injectable } from '@angular/core';
// import { CanActivate } from '@angular/router';

// @Injectable()
// export class AuthGuard implements CanActivate{

//     constructor(private authService: AuthService,
//         private router: Router) {}

// canActivate(route: ActivatedRouteSnapshot,
//         state: RouterStateSnapshot): Observable<boolean> | Promise <boolean> | boolean {
// return this.authService.isAuthenticated().then(
//     (authenticated: boolean) => {
//         if (authenticated) {
//             return true;
//         } else  {
//             // tslint:disable-next-line: no-unused-expression
//             this.router.navigate(['/']);
//             return false;
//         }
//     }
// );
// }
// }