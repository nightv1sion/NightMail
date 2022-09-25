import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-blue-link',
  templateUrl: './blue-link.component.html',
  styleUrls: ['./blue-link.component.css']
})
export class BlueLinkComponent implements OnInit {

  @Input() routerLink: string = "";
  @Input() textLink: string = "";

  constructor() { }

  ngOnInit(): void {
  }

}
