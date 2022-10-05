import { Component, Input, OnInit } from '@angular/core';
import { Folder } from 'src/app/data/models/Folder';
import { FolderService } from 'src/app/services/folder.service';

@Component({
  selector: 'app-folder',
  templateUrl: './folder.component.html',
  styleUrls: ['./folder.component.css']
})
export class FolderComponent implements OnInit {

  // @Input() folder!: string;
  @Input() folder!: Folder;



  constructor(public folderService: FolderService) { 

  }

  ngOnInit(): void {
  }

  setThisFolder(){
    this.folderService.setFolder(this.folder);
  }
}

