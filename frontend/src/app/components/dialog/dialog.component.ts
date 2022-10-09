import { Component, Input, OnInit } from '@angular/core';
import { MatDialogRef} from '@angular/material/dialog';
import { Folder } from 'src/app/data/models/Folder';


@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.css']
})
export class DialogComponent implements OnInit {

  @Input() folder!: Folder;
  @Input() onSave!: (folder: Folder) => void;
  @Input() onClose!: () => void;

  constructor(public dialogRef: MatDialogRef<DialogComponent>) { }

  ngOnInit(): void {
  }

  onSubmit(){
    this.onSave(this.folder);
    this.onClose();
  }


}
