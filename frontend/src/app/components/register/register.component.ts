import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgModel } from '@angular/forms';
import { RouteReuseStrategy } from '@angular/router';
import { map, catchError } from 'rxjs';
import { UserForRegistrationDto } from 'src/app/data/datatransferbojects/UserForRegistration';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  isError: boolean = false;

  user: UserForRegistrationDto = new UserForRegistrationDto();

  constructor(private authentication: AuthenticationService) { }

  ngOnInit(): void {
  }

  onSubmit(){
      this.authentication.registerUser(this.user).pipe(map((data: any) => {
        console.log(data);
        return data;
      }), 
       catchError(err => {
        console.log(err);
        return [];
       }));
    return false;
  }

  matchingPassword(password: NgModel, passwordConfirmation: NgModel){
    if(password.value !== passwordConfirmation.value)
      return false;

    return true;
  }

  buttonIsDisabled(){

  }

}
