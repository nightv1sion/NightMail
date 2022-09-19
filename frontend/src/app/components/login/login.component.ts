import { Component, OnInit } from '@angular/core';
import { UserForLogin } from 'src/app/data/datatransferbojects/UserForLogin';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user: UserForLogin = new UserForLogin();

  constructor(private authentication: AuthenticationService) { }

  ngOnInit(): void {
  }

  onSubmit(){
    this.authentication.loginUser(this.user).subscribe(result => console.log(result));
    return false;
  }

}
