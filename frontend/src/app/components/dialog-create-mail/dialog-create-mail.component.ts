import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-dialog-create-mail',
  templateUrl: './dialog-create-mail.component.html',
  styleUrls: ['./dialog-create-mail.component.css']
})
export class DialogCreateMailComponent implements OnInit {

  receiverMail: string = "";
  mailText: string = "";
  mailSubject: string = ""; 

  @Input() onCreate!: (receiverMail: string, mailText: string, mailSubject: string) => void;
  @Input() onClose!: () => void; 

  constructor() { }

  ngOnInit(): void {
  }

  onSubmit(){
    this.onCreate(this.receiverMail, this.mailText, this.mailSubject);
    this.onClose();
  }

}
