import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserForLogin } from 'src/app/data/datatransferbojects/UserForLogin';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  errorMessage: string = "";

  user: UserForLogin = new UserForLogin();

  constructor(private authentication: AuthenticationService, private router: Router) {
    this.onSubmit = this.onSubmit.bind(this);
    if(authentication.isAuthenticated())
      router.navigate([""]);
  }

  ngOnInit(): void {
  }

  clearErrorMessage(){
    this.errorMessage = "";
  }

  onSubmit(){
    this.authentication.loginUser(this.user, (error) => 
    { 
      console.log("error"); console.log(error); 
      if(error["status"] == 404 || error["status"]== 400 || error["status"] == 401)
        this.errorMessage = "Email or password is wrong";
      else
        this.errorMessage = "Something went wrong when posting to the server";
    }, () => {
      this.router.navigate([""]);
    });
    return false;
  }
}
