import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserForLogin } from '../data/datatransferbojects/UserForLogin';
import { UserForRegistrationDto } from '../data/datatransferbojects/UserForRegistration';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  authUrl = environment.apiUrl + "/auth";

  registerUser(user: UserForRegistrationDto){
    return this.http.post(this.authUrl + "/register", user);
  }

  loginUser(user: UserForLogin){
    return this.http.post(this.authUrl + "/login", user);
  }

  constructor(private http: HttpClient) {

   }
}
