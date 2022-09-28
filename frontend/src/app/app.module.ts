import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { AuthenticationComponent } from './components/authentication/authentication.component';
import { UserManagementComponent } from './components/user-management/user-management.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './services/auth-interceptor.service';
import { UserService } from './services/user.service';
import { AuthenticationService } from './services/authentication.service';
import { BlueLinkComponent } from './components/blue-link/blue-link.component';
import { BorderedButtonComponent } from './components/bordered-button/bordered-button.component';
import { DisabledBorderedButtonComponent } from './components/disabled-bordered-button/disabled-bordered-button.component';
import { ErrorMessageComponent } from './components/error-message/error-message.component';
import { RoundUserImageComponent } from './components/round-user-image/round-user-image.component';
import { RoundUserInitialsComponent } from './components/round-user-initials/round-user-initials.component';
import { PasswordConfirmationComponent } from './components/password-confirmation/password-confirmation.component';
import { EditUserInfoComponent } from './components/edit-user-info/edit-user-info.component';

export function tokenGetter(){
  return localStorage.getItem("JwtToken");
}

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    AuthenticationComponent,
    UserManagementComponent,
    LoginComponent,
    RegisterComponent,
    BlueLinkComponent,
    BorderedButtonComponent,
    DisabledBorderedButtonComponent,
    ErrorMessageComponent,
    RoundUserImageComponent,
    RoundUserInitialsComponent,
    PasswordConfirmationComponent,
    EditUserInfoComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [UserService,
    AuthenticationService, 
    {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
    }, 
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
