import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
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

  folderEmitter = new EventEmitter<Folder>();

  constructor(private http: HttpClient) {
  }

  getFolders(handlers: RequestHandlers){
    return this.http.get<Folder[]>(environment.apiUrl + "/folder/all").subscribe({
      next: (data: Folder[]) => {if(handlers.nextHandler) handlers.nextHandler(data); this.isIncoming = true;},
      error: (error) => {if(handlers.errorHandler) handlers.errorHandler(error)}
    });
  }

  setFolder(folder: Folder){
    this.currentFolder = folder;
    this.isIncoming = false;
    this.isOutgoing = false;
    this.folderEmitter.emit();
  }

  setIncomingFolder(){
    this.isIncoming = true;
    this.isOutgoing = false;    
    this.currentFolder = null;
    this.folderEmitter.emit();
  }

  setOutgoingFolder(){
    this.isIncoming = false;
    this.isOutgoing = true;
    this.currentFolder = null;
    this.folderEmitter.emit();
  }
  
}
