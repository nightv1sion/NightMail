import { Component, Input, OnInit } from '@angular/core';
import { Folder } from 'src/app/data/models/Folder';
import { FolderService } from 'src/app/services/folder.service';

@Component({
  selector: 'app-personal-folder-list',
  templateUrl: './personal-folder-list.component.html',
  styleUrls: ['./personal-folder-list.component.css', '../folder/folder.component.css']
})
export class PersonalFolderListComponent implements OnInit {

  folders: Folder[] = [];

  constructor(private folderService: FolderService) { }

  ngOnInit(): void {
    this.folderService.getFolders({
      nextHandler: (data: Folder[]) => this.folders = data,
      errorHandler: (error: any) => console.log("Something went wrong when getting folders from the server: ", error)
    });
  }

}
