import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { of, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { tokenGetter } from '../app.module';
import { User } from '../data/models/User';
import { AuthenticationService } from './authentication.service';

@Injectable({providedIn: "root"})
export class UserService {

  userConfirmation: boolean = false;

  subjectNotifier: Subject<null> = new Subject<null>();

  notifyAboutChange(){
    this.subjectNotifier.next(null);
  }

  getUser(handlers: RequestHandlers){
    return this.http.get(environment.apiUrl + "/user").subscribe({
      next: (data) => {if(handlers.nextHandler) handlers.nextHandler(data)},
      error: (error) => {if(handlers.errorHandler) handlers.errorHandler(error); this.authService.logOutUser();}
    });
  }

  getPhotoForUser(){
      return this.http.get(environment.apiUrl + "/image/user", {responseType: "blob"});
  }
  
  constructor(private http: HttpClient, private router: Router, private authService: AuthenticationService) {
   }

   confirmUserPassword(password: string, handlers: RequestHandlers){
    return this.http.post(environment.apiUrl + "/Auth/confirm-user", password).subscribe({
      next: (data) => {if(handlers.nextHandler) handlers.nextHandler(data); this.userConfirmation = true;},
      error: (data) => {if(handlers.errorHandler) handlers.errorHandler(data); this.userConfirmation = false;}
   });
   }
}

export interface RequestHandlers {
  nextHandler?: Function,
  errorHandler?: Function
}
