import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
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

  loginUser(user: UserForLogin, errorHandler: (error: any) => void){
    return this.http.post(this.authUrl + "/login", user).subscribe({
      next: (data:any) => {console.log(data); this.setToken(data);},
      error: errorHandler
    });
  }

  getToken(){
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
    const decodedClaims:any = jwtDecode(data["accessToken"]);
    const decodedExpiration:string = data["expiration"];
    if(decodedClaims && decodedExpiration)
    {
      this.userService.setUser(new Date(decodedExpiration));
    }
  }

  constructor(private http: HttpClient, private userService: UserService) {
   }
   
}
