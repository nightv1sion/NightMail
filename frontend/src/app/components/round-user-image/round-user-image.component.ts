import { Component, Input, OnInit } from '@angular/core';
import { SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-round-user-image',
  templateUrl: './round-user-image.component.html',
  styleUrls: ['./round-user-image.component.css']
})
export class RoundUserImageComponent implements OnInit {

  @Input() image?: SafeUrl;

  constructor() { }

  ngOnInit(): void {
  }

}
