import { Component, OnInit } from '@angular/core';
import { SafeUrl } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { User } from 'src/app/data/models/User';
import { ImageService } from 'src/app/services/image.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-password-confirmation',
  templateUrl: './password-confirmation.component.html',
  styleUrls: ['./password-confirmation.component.css']
})
export class PasswordConfirmationComponent implements OnInit {

  user?: User;
  userImage?: SafeUrl;

  errorMessage?: string;


  password: string = "";

  constructor(private userService: UserService, private router: Router, private imageService: ImageService) {
      
   }

  ngOnInit(): void {
    this.userService.getUser({
      nextHandler: (data: any) => {this.user = data; this.userService.getPhotoForUser().subscribe({next: (data) => this.userImage = this.imageService.getPhotoUrl(data)})},
      errorHandler: (error: any) => {this.router.navigate([""]);}
    })
  }

  onSubmit(){
    this.userService.confirmUserPassword(this.password, {nextHandler: (data: any) => this.router.navigate(["/user/edit"]), errorHandler: (data: any) => this.errorMessage = "Password is wrong"});
    return false;
  }

}
