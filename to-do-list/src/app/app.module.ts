import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ListComponent } from './list/list.component';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { HeaderComponent } from './header/header.component';
import { RegisterComponent } from './register/register.component';
import { UsersComponent } from './users/users.component';
import { ServiceSharedService } from './service-shared.service';
import { TokenInterceptor } from './token-interceptor.interceptor';
import { AuthService } from './auth.service';
import { AuthGuard } from './AuthGuard.service';


@NgModule({
  declarations: [
    AppComponent,
    ListComponent,
    LoginComponent,
    HeaderComponent,
    RegisterComponent,
    UsersComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule, 
    FormsModule,
    HttpClientModule
  ],
  providers: [ServiceSharedService,AuthService,AuthGuard,
  {
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
