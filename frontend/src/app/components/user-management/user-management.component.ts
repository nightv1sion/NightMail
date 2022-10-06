import { Component, OnInit } from '@angular/core';
import { SafeUrl } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { User } from 'src/app/data/models/User';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ImageService } from 'src/app/services/image.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {

  user?: User;

  userImage?: SafeUrl;

  constructor(public userService: UserService, private authService: AuthenticationService, private imageService: ImageService, private router: Router) {
  }

  logOut(){
    console.log("logOut");
    this.authService.logOutUser();
    this.user = undefined;
    this.userImage = undefined;
    this.userService.notifyAboutChange();
    return false;
  }

  goToEditProfile(){
    this.router.navigate(["auth","user", "edit"])
  }

  ngOnInit(): void {
    if(this.authService.isAuthenticated())
      {
        this.userService.getUser({
          nextHandler: (data: any) => 
            {this.user = data; 
              this.userService.getPhotoForUser().subscribe(data => this.userImage = this.imageService.getPhotoUrl(data)); this.userService.notifyAboutChange();}, 
          errorHandler: (err: any) => {
            this.logOut();
          }});
      }
  }

}
