import { Component, Input, OnInit } from '@angular/core';
import { Mail } from 'src/app/data/models/Mail';

@Component({
  selector: 'app-mail',
  templateUrl: './mail.component.html',
  styleUrls: ['./mail.component.css']
})
export class MailComponent implements OnInit {

  @Input() mail!: Mail;

  constructor() { }

  ngOnInit(): void {
  }

}
