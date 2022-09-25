import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { tokenGetter } from '../app.module';
import { User } from '../data/models/User';
import { AuthenticationService } from './authentication.service';

@Injectable({providedIn: "root"})
export class UserService {

  public user?: User;
  
  async setUser(){
    await this.getUserFromServer();
    await this.getPhotoForUser();
    this.router.navigate([""]); 
  }
  
  private async getUserFromServer(){
    await this.http.get(environment.apiUrl + "/user").subscribe(
      {
        next: (data: any) => {this.user = data; console.log("next of getting user");console.log(this.user)},
        error: (err) => {
          
          console.log(err);
          if(err.status == 401) console.log("User is expired");
          else console.log("Something went wrong when getting user from server: " + err);
        }
      }
    );
  }

  removeUser(){
    this.user = undefined;
  }

  public getPhotoForUser(){
      return this.http.get(environment.apiUrl + "/image/user", {responseType: "blob"}).subscribe({
        next: (data: any) => {
          this.user!.imageUrl = URL.createObjectURL(data);
          console.log(data)},
        error: (err) => console.log(err)
      });
  }

  public getPhotoUrl(){
    return this.sanitizer.bypassSecurityTrustUrl(this.user!.imageUrl);
  }
  
  constructor(private http: HttpClient, private router: Router, private sanitizer: DomSanitizer) {
    const stringToken = localStorage.getItem("JwtToken");
    console.log("token = " + stringToken);
    if(!stringToken)
      return;
    const token = JSON.parse(stringToken);
    console.log(token);
    if(token)
      this.setUser();
   }
}
