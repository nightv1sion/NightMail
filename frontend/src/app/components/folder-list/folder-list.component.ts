import { Component, OnInit } from '@angular/core';
import { Folder } from 'src/app/data/models/Folder';
import { FolderService } from 'src/app/services/folder.service';

@Component({
  selector: 'app-folder-list',
  templateUrl: './folder-list.component.html',
  styleUrls: ['./folder-list.component.css']
})
export class FolderListComponent implements OnInit {
  

  constructor(private folderService: FolderService) { }

  ngOnInit(): void {
    
  }

  chooseFolder(folder: Folder){
    this.folderService.setFolder(folder);
  }

  chooseIncomingFolder(){
    this.folderService.setIncomingFolder();
  }

  chooseOutgoingFolder(){
    this.folderService.setOutgoingFolder();
  }

}
