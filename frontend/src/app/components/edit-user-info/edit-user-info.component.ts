import { Component, OnInit } from '@angular/core';
import { FormControl, NgModel } from '@angular/forms';
import { SafeUrl } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { UserForEditDto } from 'src/app/data/datatransferbojects/UserForEditDto';
import { User } from 'src/app/data/models/User';
import { ImageService } from 'src/app/services/image.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-edit-user-info',
  templateUrl: './edit-user-info.component.html',
  styleUrls: ['./edit-user-info.component.css']
})
export class EditUserInfoComponent implements OnInit {

  userForEdit?: UserForEditDto;
  user?: User;
  userImage?: SafeUrl;
  newUserImage?: Blob;

  constructor(public userService: UserService, private router: Router, private imageService: ImageService) {
      // if(!userService.userConfirmation)
      // router.navigate(["user/passwordconfirmation"]);
  }

  ngOnInit(): void {
    this.userService.getUser(({
      nextHandler: (data: any) => {this.user = data; this.userService.getPhotoForUser().subscribe({next: (data) => this.userImage = this.imageService.getPhotoUrl(data)})},
      errorHandler: (error: any) => {this.router.navigate([""]);}
    }))
    this.userService.getUserForEdit({
      nextHandler: (data: any) => {this.userForEdit = data; console.log(data.substring(0, 10))},
      errorHandler: (error: any) => {console.log("Something went wrong when getting user for edit information from server"); this.router.navigate([""]);} 
    })
  }

  birthdayIsValid(){
    let dateNow:Date = new Date(Date.now());
    let dateMax:Date = new Date(dateNow);
    dateMax.setFullYear(dateNow.getFullYear() + 100);
    let dateMin:Date = new Date(dateNow);
    dateMin.setFullYear(dateNow.getFullYear() - 100);
    
    if(this.userForEdit!.birthday != undefined){
      let birthdayDate = new Date(this.userForEdit!.birthday);
      if(birthdayDate.getTime() < dateMin.getTime() || birthdayDate.getTime() > dateMax.getTime())
      {
        return false;
      }
    }
    else {
      return false;
    }
    return true;
  }

  onSubmit(file: HTMLInputElement){
    console.log(file.files);

    return false;
  }

  changeImage(fileInput: HTMLInputElement){
    if(fileInput.files && fileInput.files[0]){
      this.userImage = this.imageService.getPhotoUrl(fileInput.files[0]);
    }
  }

}
