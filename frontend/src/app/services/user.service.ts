import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { of, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { tokenGetter } from '../app.module';
import { UserForEditDto } from '../data/datatransferbojects/UserForEditDto';
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
      next: (data) => {if(handlers.nextHandler) handlers.nextHandler(data); console.log(data);},
      error: (error) => {if(handlers.errorHandler) 
        handlers.errorHandler(error); 
        this.authService.logOutUser(); 
        this.userConfirmation = false;}
    });
  }

  getUserForEdit(handlers: RequestHandlers){
    return this.http.get(environment.apiUrl + "/user/for-edit").subscribe({
      next: (data) => {if(handlers.nextHandler) handlers.nextHandler(data);},
      error: (error) => {if(handlers.errorHandler) 
        handlers.errorHandler(error);
    }});
  }

  getPhotoForUser(){
      return this.http.get(environment.apiUrl + "/image/user", {responseType: "blob"});
  }

  putUser(handlers: RequestHandlers, data: UserForEditDto){
    return this.http.put(environment.apiUrl+"/user", data).subscribe({
      next: (data) => {if(handlers.nextHandler) handlers.nextHandler(data);},
      error: (error) => {if(handlers.errorHandler) handlers.errorHandler(error);}
    });
  }

  postPhotoForUser(data: Blob, handlers: RequestHandlers){
    let formData = new FormData();
    formData.append('image', data);
    return this.http.post(environment.apiUrl + "/image/user", formData).subscribe({
      next: (data) => {if(handlers.nextHandler) handlers.nextHandler(data);},
      error: (error) => {if(handlers.errorHandler) handlers.errorHandler(error);}
    });
  }

  deletePhotoForUser(handlers: RequestHandlers){
    return this.http.delete(environment.apiUrl + "/image/user").subscribe({
      next: (data) => {if(handlers.nextHandler) handlers.nextHandler(data);},
      error: (error) => {if(handlers.errorHandler) handlers.errorHandler(error);}
    });
  }

  constructor(private http: HttpClient, private router: Router, private authService: AuthenticationService) {
   }

   confirmUserPassword(password: string, handlers: RequestHandlers){
    return this.http.post(environment.apiUrl + "/Auth/confirm-user",JSON.stringify(password), { headers: { "Content-Type": "application/json"}}).subscribe({
      next: (data) => {if(handlers.nextHandler) handlers.nextHandler(data); this.userConfirmation = true;},
      error: (data) => {if(handlers.errorHandler) handlers.errorHandler(data); console.log(data); this.userConfirmation = false;}
   });
   }
}

export interface RequestHandlers {
  nextHandler?: Function,
  errorHandler?: Function
}
