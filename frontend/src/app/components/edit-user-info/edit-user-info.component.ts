import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, NgModel } from '@angular/forms';
import { SafeUrl } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
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

  @ViewChild("newImage") fileUploader!: ElementRef;

  userForEdit?: UserForEditDto;
  user?: User;
  userImage?: SafeUrl;
  newUserImage?: Blob;
  userImageExistsInitially: boolean = false;

  notifierSubscription: Subscription = this.userService.subjectNotifier.subscribe(() => {
    this.getUserInfo();
  })


  constructor(public userService: UserService, private router: Router, private imageService: ImageService) {
      
    if(!userService.userConfirmation)
    {
      router.navigate(["user/passwordconfirmation"]);
      console.log("User not confirmed");      
    }    
    else {
      console.log("User is confirmed");
    }
      this.onSubmit = this.onSubmit.bind(this);
  }

  ngOnInit(): void {
    this.getUserInfo();
  }

  getUserInfo(){
    this.userService.getUser(({
      nextHandler: (data: any) => {this.user = data; this.userService.getPhotoForUser().subscribe({next: (data) => {this.userImage = this.imageService.getPhotoUrl(data); this.userImageExistsInitially = true;}})},
      errorHandler: (error: any) => {this.router.navigate([""]);}
    }))
    this.userService.getUserForEdit({
      nextHandler: (data: any) => {this.userForEdit = data; console.log(data); this.userForEdit!.birthday = new Date(data.birthday)},
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
      let birthdayDate = this.userForEdit!.birthday;
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
    if(this.userForEdit)
      this.userService.putUser({
        nextHandler: (data: any) => {this.userService.notifyAboutChange();},
        errorHandler: (error: any) => console.log("Something went wrong when posting data to the server.") 
      }, this.userForEdit);
    if(!this.userImage && this.userImageExistsInitially){
      this.userService.deletePhotoForUser({errorHandler: (error:any) => {console.log("Something went wrong when deleting image.");}, nextHandler: () => {this.userService.notifyAboutChange()}});
    }
    if(this.newUserImage)
    {
      if(this.userImageExistsInitially)
        this.userService.deletePhotoForUser({
          nextHandler: (data:any) => {this.userService.postPhotoForUser(this.newUserImage!, {nextHandler: () => {this.userService.notifyAboutChange();}, errorHandler: (error: any) => {console.log("Something went wrong w!!!hen posting image to the server.");}})},
          errorHandler: (error:any) => {console.log("Something went wrong when deleting image.")}});
      else
        this.userService.postPhotoForUser(this.newUserImage!, {nextHandler: () => {this.userService.notifyAboutChange();},errorHandler: (error: any) => {console.log("Something went wrong when posting image to the server.");}})
    }
    return false;
  }

  changeImage(fileInput: HTMLInputElement){
    if(fileInput.files && fileInput.files[0]){
      this.userImage = this.imageService.getPhotoUrl(fileInput.files[0]);
      this.newUserImage = fileInput.files[0];
    }
  }

  dateForInput(date?: Date){
    const month = ("0" + (this.userForEdit!.birthday!.getMonth()+1)).slice(-2);
    const day = ("0" + this.userForEdit!.birthday!.getDate()).slice(-2);
    let str = `${this.userForEdit!.birthday?.getFullYear()}-${month}-${day}`;
    return str;
  }

  changeBirthday(elem: HTMLInputElement){
    this.userForEdit!.birthday = new Date(elem.value);
    console.log(this.userForEdit?.birthday);
  }

  deleteImage(){
    this.userImage = undefined;
    if(this.fileUploader.nativeElement.value)
      this.fileUploader.nativeElement.value = null;
  }

}
