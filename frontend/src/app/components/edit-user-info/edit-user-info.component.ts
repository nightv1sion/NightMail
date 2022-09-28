import { Component, OnInit } from '@angular/core';
import { SafeUrl } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { User } from 'src/app/data/models/User';
import { ImageService } from 'src/app/services/image.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-edit-user-info',
  templateUrl: './edit-user-info.component.html',
  styleUrls: ['./edit-user-info.component.css']
})
export class EditUserInfoComponent implements OnInit {

  user?: User;
  userImage?: SafeUrl;

  constructor(public userService: UserService, private router: Router, private imageService: ImageService) {
      if(!userService.userConfirmation)
      router.navigate(["user/passwordconfirmation"]);
  }

  ngOnInit(): void {
    this.userService.getUser(({
      nextHandler: (data: any) => {this.user = data; this.userService.getPhotoForUser().subscribe({next: (data) => this.userImage = this.imageService.getPhotoUrl(data)})},
      errorHandler: (error: any) => {this.router.navigate([""]);}
    }))
  }

}
