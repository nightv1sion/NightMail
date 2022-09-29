import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgModel } from '@angular/forms';
import { Router, RouteReuseStrategy } from '@angular/router';
import { map, catchError, Observable, of } from 'rxjs';
import { UserForRegistrationDto } from 'src/app/data/datatransferbojects/UserForRegistration';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  errorMessage: string = "";

  user: UserForRegistrationDto = new UserForRegistrationDto();

  constructor(private authentication: AuthenticationService, private router: Router) {
    this.onSubmit = this.onSubmit.bind(this);
    if(authentication.isAuthenticated())
      router.navigate([""]);
  }

  clearErrorMessage(){
  this.errorMessage = "";
  }

  ngOnInit(): void {
  } 

  onSubmit(): boolean{
    this.authentication.registerUser(this.user).subscribe(
      {next: (data) => {console.log("all is done"); console.log(data); this.router.navigate(['login'])},
      error: error => { 
        if(error["error"]["StatusCode"] == 409) 
          this.errorMessage = error["error"]["Message"];
        else
          this.errorMessage = "Something went wrong when posting to the server";
        }}
    )
      
    return false;
  }

  matchingPassword(password: NgModel, passwordConfirmation: NgModel){
    if(password.value !== passwordConfirmation.value)
      return false;

    return true;
  }

  birthdayIsValid(){
    let dateNow:Date = new Date(Date.now());
    let dateMax:Date = new Date(dateNow);
    dateMax.setFullYear(dateNow.getFullYear() + 100);
    let dateMin:Date = new Date(dateNow);
    dateMin.setFullYear(dateNow.getFullYear() - 100);
    
    if(this.user.birthday != undefined){
      let birthdayDate = new Date(this.user.birthday);
      if(birthdayDate.getTime() < dateMin.getTime() || birthdayDate.getTime() > dateMax.getTime())
      {
        return false;
      }
    }
    else {
      return false;
    }
    return true;
  }

}
