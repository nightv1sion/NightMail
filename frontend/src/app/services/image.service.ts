import { Injectable } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  public getPhotoUrl(blob: Blob){
    let imageUrl = URL.createObjectURL(blob)
    return this.sanitizer.bypassSecurityTrustUrl(imageUrl);
  }

  constructor(private sanitizer: DomSanitizer) { }
}
