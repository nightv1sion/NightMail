import { Component, Injector, Input, OnInit, ViewChild } from '@angular/core';
import { Folder } from 'src/app/data/models/Folder';
import { FolderService } from 'src/app/services/folder.service';
import {MatMenuTrigger} from '@angular/material/menu';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-folder',
  templateUrl: './folder.component.html',
  styleUrls: ['./folder.component.css']
})
export class FolderComponent implements OnInit {

  // @Input() folder!: string;
  @Input() folder!: Folder;

  // we create an object that contains coordinates 
  menuTopLeftPosition =  {x: '0', y: '0'} 
 
  // reference to the MatMenuTrigger in the DOM 
  @ViewChild(MatMenuTrigger, {static: true}) matMenuTrigger!: MatMenuTrigger; 
 
  
  onRightClick(event: MouseEvent) { 
      event.preventDefault(); 
 
      this.menuTopLeftPosition.x = event.clientX + 'px'; 
      this.menuTopLeftPosition.y = event.clientY + 'px'; 
 
      
      this.matMenuTrigger.openMenu(); 
 
  } 

  onDelete(){
    this.folderService.deleteFolder({
    }, this.folder)
  }


  constructor(public folderService: FolderService, private injector: Injector) { 

  }

  ngOnInit(): void {
  }

  setThisFolder(){
    this.folderService.setFolder(this.folder);
  }
  onMouseRight(){
    
  }

  onEdit(){
    let dialog = this.injector.get(MatDialog);
    let dialogRef = dialog.open(DialogComponent);
    dialogRef.componentInstance.folder = this.folder;
    dialogRef.componentInstance.onSave = (folder: Folder) => {
      this.folderService.putFolder({
      }, this.folder);
    };

    dialogRef.componentInstance.onClose = () => {
      dialog.closeAll();
    }
  }
}

