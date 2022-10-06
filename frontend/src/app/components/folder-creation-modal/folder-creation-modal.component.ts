import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FolderService } from 'src/app/services/folder.service';

@Component({
  selector: 'app-folder-creation-modal',
  templateUrl: './folder-creation-modal.component.html',
  styleUrls: ['./folder-creation-modal.component.css']
})
export class FolderCreationModalComponent implements OnInit {

  errorMessage: string = "";

  name: string = "";

  constructor(private folderService: FolderService) { }

  ngOnInit(): void {
  }

  onSubmit(cancelButton: HTMLButtonElement){
    if(!this.name)
    {
      this.errorMessage = "Name is required";
      return false;
    }      

    this.folderService.postFolder({
      nextHandler: (data: any) => {this.folderService.folderEmitter.emit(); cancelButton.click();},
      errorHandler: (error: HttpErrorResponse) => {
        console.log(error);
        if(error.status == 409)
         this.errorMessage = "This name for folder exists yet";
        else
          this.errorMessage = "Something went wrong when posting to the server";
      }
    }, this.name)

    return false;
  }
}
