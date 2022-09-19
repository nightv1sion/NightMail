import { Component, OnInit } from '@angular/core';
import { UserForRegistrationDto } from 'src/app/data/datatransferbojects/UserForRegistration';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  user: UserForRegistrationDto = new UserForRegistrationDto();

  constructor(private authentication: AuthenticationService) { }

  ngOnInit(): void {
  }

  onSubmit(){
    this.authentication.registerUser(this.user).subscribe(result => console.log(result));
    return false;
  }


}
