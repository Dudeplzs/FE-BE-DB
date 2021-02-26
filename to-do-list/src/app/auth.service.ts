import { Injectable } from "@angular/core";
import { Router } from "@angular/router";

@Injectable({
    providedIn: 'root'
})
export class AuthService{
    isLoggedin = false;

    constructor(private _router: Router){}

    isAuthenticated(){
        const promise = new Promise(
            (resolve,reject) => {
                setTimeout(() => {
                    resolve(this.isLoggedin);
                },1000);
            }
        );
        return promise;
    }

    getToken(){
        return localStorage.getItem('token');
    }
    
    removeToken(){
        return localStorage.removeItem('token');
    }

    login(){
        return this.isLoggedin = (this.getToken() === null) ? false : true;
    }

    logout(){
        this.removeToken();
        return this.isLoggedin = false;
    }
}
