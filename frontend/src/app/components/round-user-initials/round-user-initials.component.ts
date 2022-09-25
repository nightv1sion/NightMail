import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-round-user-initials',
  templateUrl: './round-user-initials.component.html',
  styleUrls: ['./round-user-initials.component.css']
})
export class RoundUserInitialsComponent implements OnInit {

  @Input() firstName?: string;
  @Input() lastName?: string;

  getInitials(): string | undefined {
    if(this.firstName && this.lastName)
      return this.firstName[0] + this.lastName[0];
    return undefined;
  }

  constructor() { }

  ngOnInit(): void {
  }

}
