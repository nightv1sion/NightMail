import { Component, Input, OnInit } from '@angular/core';
import { Folder } from 'src/app/data/models/Folder';

@Component({
  selector: 'app-folder',
  templateUrl: './folder.component.html',
  styleUrls: ['./folder.component.css']
})
export class FolderComponent implements OnInit {

  @Input() folder!: string;

  constructor() { }

  ngOnInit(): void {
  }

}
