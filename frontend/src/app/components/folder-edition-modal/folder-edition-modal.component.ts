import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { Folder } from 'src/app/data/models/Folder';
import { FolderService } from 'src/app/services/folder.service';

@Component({
  selector: 'app-folder-edition-modal',
  templateUrl: './folder-edition-modal.component.html',
  styleUrls: ['../folder-creation-modal/folder-creation-modal.component.css']
})
export class FolderEditionModalComponent implements OnInit {

  errorMessage: string = "";

  @Input() folder!: Folder;

  constructor(private folderService: FolderService) { }

  ngOnInit(): void {
  }

  onSubmit(cancelButton: HTMLButtonElement){
    if(!this.folder.name)
    {
      this.errorMessage = "Name is required";
      return false;
    }      

    // this.folderService.putFolder({
    //   nextHandler: (data: any) => {this.folderService.folderEmitter.emit(); cancelButton.click();},
    //   errorHandler: (error: HttpErrorResponse) => {
    //     console.log(error);
    //     if(error.status == 409)
    //      this.errorMessage = "This name for folder exists yet";
    //     else
    //       this.errorMessage = "Something went wrong when posting to the server";
    //   }
    // }, this.folder)

    return false;
  }

}
