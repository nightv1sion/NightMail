import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { catchError, map, Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserForLogin } from '../data/datatransferbojects/UserForLogin';
import { UserForRegistrationDto } from '../data/datatransferbojects/UserForRegistration';
import jwtDecode from 'jwt-decode';
import { UserService } from './user.service';
import { User } from '../data/models/User';
import { ClaimDTO } from '../data/datatransferbojects/ClaimDTO';
import { Router } from '@angular/router';
@Injectable({providedIn: "root"})
export class AuthenticationService {

  authUrl = environment.apiUrl + "/auth";
  headers = new HttpHeaders().set('Content-Type', 'application/json');


  registerUser(user: UserForRegistrationDto){
    return this.http.post(this.authUrl + "/register", user);
  }

  isAuthenticated(){
    console.log(this.getToken() != null ? true : false);
    return this.getToken() != null ? true : false;
  }

  loginUser(user: UserForLogin, errorHandler: (error: any) => void, nextHandler:() => void){
    const userService = this.injector.get(UserService);
    return this.http.post(this.authUrl + "/login", user).subscribe({
      next: (data:any) => {console.log(data); this.setToken(data); nextHandler()},
      error: errorHandler
    });
  }

  logOutUser(){
    this.deleteToken();
    // let userService = this.injector.get(UserService);
    // userService.notifyAboutChange();
  }

  getToken() : any{
    let token = localStorage.getItem("JwtToken");
    if(token)
    {
      let jwtToken = JSON.parse(token);
      return jwtToken;
    }  
    return null;
  }

  setToken(data: any){
    localStorage.setItem("JwtToken", JSON.stringify(data));
  }

  private deleteToken(){
    localStorage.removeItem("JwtToken");
  }

  constructor(private http: HttpClient, private injector: Injector) {
  }
   
}
