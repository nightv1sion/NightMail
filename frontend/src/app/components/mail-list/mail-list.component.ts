import { Component, OnInit } from '@angular/core';
import { Mail } from 'src/app/data/models/Mail';
import { FolderService } from 'src/app/services/folder.service';
import { MailService } from 'src/app/services/mail.service';

@Component({
  selector: 'app-mail-list',
  templateUrl: './mail-list.component.html',
  styleUrls: ['./mail-list.component.css']
})
export class MailListComponent implements OnInit {

  mails: Mail[] = [];

  constructor(private mailService: MailService, private folderService: FolderService) {
    
   }

  ngOnInit(): void {
    this.getMails();
    this.folderService.folderEmitter.pipe().subscribe(r => this.getMails());
  }

  private getMails(){
    this.mailService.getMails({
      nextHandler: (data: Mail[]) => this.mails = data,
      errorHandler: (error: any) => console.log("Something went wrong when getting mails from the server: " + error)
    });
  }
}
