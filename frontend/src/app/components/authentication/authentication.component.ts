import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/data/models/User';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})
export class AuthenticationComponent implements OnInit {

  constructor(public userService: UserService) { 
  }

  printUser(){
    console.log(this.userService.user);
  }

  

  ngOnInit(): void {
  }


}
