import { Component, OnInit } from '@angular/core';
import { FolderService } from 'src/app/services/folder.service';

@Component({
  selector: 'app-outgoing-folder',
  templateUrl: './outgoing-folder.component.html',
  styleUrls: ['../folder/folder.component.css']
})
export class OutgoingFolderComponent implements OnInit {

  constructor(public folderService: FolderService) { }

  ngOnInit(): void {
  }

  setThisFolder(){
    this.folderService.setOutgoingFolder();
  }

}
