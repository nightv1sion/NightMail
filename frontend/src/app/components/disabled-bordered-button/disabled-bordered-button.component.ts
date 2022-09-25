import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-disabled-bordered-button',
  templateUrl: './disabled-bordered-button.component.html',
  styleUrls: ['./disabled-bordered-button.component.css']
})
export class DisabledBorderedButtonComponent implements OnInit {

  @Input() textButton?: string;

  constructor() { }

  ngOnInit(): void {
  }

}
