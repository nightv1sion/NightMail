import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { RequestHandlers } from '../shared/RequestHandlers';
import { FolderService } from './folder.service';

@Injectable({
  providedIn: 'root'
})
export class MailService {

  constructor(private http: HttpClient, private folderService: FolderService) {
    
  }

  getMails(handlers: RequestHandlers){
    if(this.folderService.isIncoming)
      return this.getIncomingMails(handlers);
    if(this.folderService.isOutgoing)
      return this.getOutgoingMails(handlers);

    if(this.folderService.currentFolder)
      return this.getMailsFromCreatedFolder(handlers);

    throw Error("There is not choosed folder for mails");
  }

  private getMailsFromCreatedFolder(handlers: RequestHandlers){
    if(!this.folderService.currentFolder)
      throw Error("There is not current folder");
    
    const folderId = this.folderService.currentFolder.folderId;
    return this.http.get(environment.apiUrl + "/mail/" + folderId).subscribe({
      next: (data) => {if(handlers.nextHandler) handlers.nextHandler(data)},
      error: (error) => {if(handlers.errorHandler) handlers.errorHandler(error)}
    })
  }

  private getOutgoingMails(handlers: RequestHandlers){
    return this.http.get(environment.apiUrl + "/mail/outgoing").subscribe({
      next: (data) => {if(handlers.nextHandler) handlers.nextHandler(data)},
      error: (error) => {if(handlers.errorHandler) handlers.errorHandler(error)}
    });
  }

  private getIncomingMails(handlers: RequestHandlers){
    return this.http.get(environment.apiUrl + "/mail/incoming").subscribe({
      next: (data) => {if(handlers.nextHandler) handlers.nextHandler(data)},
      error: (error) => {if(handlers.errorHandler) handlers.errorHandler(error)}
    });
  }
}
