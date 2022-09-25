import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {

  constructor(public userService: UserService, private authService: AuthenticationService) { }

  logOut(){
    console.log("logOut");
    this.authService.logOutUser();
    return false;
  }

  ngOnInit(): void {
  }

}
