import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-bordered-button',
  templateUrl: './bordered-button.component.html',
  styleUrls: ['./bordered-button.component.css']
})
export class BorderedButtonComponent implements OnInit {

  @Input() onSubmit?: () => boolean;
  @Input() textButton?: string;

  constructor() { }

  ngOnInit(): void {
  }

}
