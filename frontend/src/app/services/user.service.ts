import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { User } from '../data/models/User';

@Injectable({providedIn: "root"})
export class UserService {

  public tempUser = new User("TempName", "TempLastName", "TempMail");

  private user?: User;

  private userExpires?: Date;

  getUser() : User | undefined{
    return this.user;
  }

  setUser(expiresTime: Date){
    const nowDate = new Date().getTime();

    if(expiresTime.getTime() < nowDate)
      return;
    
    this.userExpires = new Date(expiresTime);

    this.getUserFromServer();
  }

  private getUserFromServer(){
    this.http.get(environment.apiUrl + "/user").subscribe(
      {
        next: (data: any) => {this.user = data; this.router.navigate([""]); console.log(this.user)},
        error: (err: HttpErrorResponse) => {
          if(err.status == 401) console.log("User is expired");
          else console.log("Something went wrong when getting user from server");
        }
      }
    )
  }

  constructor(private http: HttpClient, private router: Router) { }
}
