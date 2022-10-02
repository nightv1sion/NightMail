import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Folder } from '../data/models/Folder';
import { RequestHandlers } from '../shared/RequestHandlers';

@Injectable({
  providedIn: 'root'
})
export class FolderService {

  currentFolder: Folder | null = null;

  isIncoming: boolean = true;
  isOutgoing: boolean = false;

  constructor(private http: HttpClient) {

  }

  getFolders(handlers: RequestHandlers){
    return this.http.get<Folder[]>(environment.apiUrl + "/folder").subscribe({
      next: (data: Folder[]) => {if(handlers.nextHandler) handlers.nextHandler(data); this.isIncoming = true;},
      error: (error) => {if(handlers.errorHandler) handlers.errorHandler(error)}
    });
  }

  setFolder(folder: Folder){
    this.currentFolder = folder;
    this.isIncoming = false;
    this.isOutgoing = false;
  }

  setIncomingFolder(){
    this.isIncoming = true;
    this.isOutgoing = false;    
    this.currentFolder = null;
  }

  setOutgoingFolder(){
    this.isIncoming = false;
    this.isOutgoing = true;
    this.currentFolder = null;
  }
  
}
