import { Component, OnInit } from '@angular/core';
import { FolderService } from 'src/app/services/folder.service';

@Component({
  selector: 'app-incoming-folder',
  templateUrl: './incoming-folder.component.html',
  styleUrls: ['../folder/folder.component.css']
})
export class IncomingFolderComponent implements OnInit {

  constructor(public folderService: FolderService) { }

  ngOnInit(): void {
  }

  setThisFolder(){
    this.folderService.setIncomingFolder();
  }

}
