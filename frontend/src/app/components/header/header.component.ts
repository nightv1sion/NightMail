import { Component, OnDestroy, OnInit } from '@angular/core';
import { SafeUrl } from '@angular/platform-browser';
import { Subscription } from 'rxjs';
import { User } from 'src/app/data/models/User';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ImageService } from 'src/app/services/image.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy {

  notifierSubscription: Subscription = this.userService.subjectNotifier.subscribe(() => {
    this.userService.getUser({
      nextHandler: (data: any) => {this.user = data; this.userService.getPhotoForUser().subscribe({next: (data: any) => this.userImage = this.imageService.getPhotoUrl(data)})},
      errorHandler: (err: any) => {
        this.user = undefined;
      }
    })
  })


  user?: User;

  userImage?: SafeUrl;

  constructor(public userService: UserService, private imageService: ImageService, private authService: AuthenticationService) {
   
  }

  ngOnInit(): void {
    if(this.authService.isAuthenticated()){
      this.userService.getUser({
        nextHandler: (data: any) => {this.user = data; this.userService.getPhotoForUser().subscribe({next: (data: any) => this.userImage = this.imageService.getPhotoUrl(data)})},
        errorHandler: (err: any) => {
          this.user = undefined;
        }
      })
    }
  }

  ngOnDestroy(): void {
    this.notifierSubscription.unsubscribe();
  }

}
