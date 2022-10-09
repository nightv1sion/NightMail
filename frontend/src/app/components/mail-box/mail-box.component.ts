import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { MailDTO } from 'src/app/data/datatransferbojects/MailDTO';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { MailService } from 'src/app/services/mail.service';
import { DialogCreateMailComponent } from '../dialog-create-mail/dialog-create-mail.component';

@Component({
  selector: 'app-mail-box',
  templateUrl: './mail-box.component.html',
  styleUrls: ['./mail-box.component.css']
})
export class MailBoxComponent implements OnInit {

  constructor(private authService: AuthenticationService, private router: Router, private dialog: MatDialog, private mailService: MailService) {
    
   }

  ngOnInit(): void {
    if(!this.authService.isAuthenticated()){
      this.router.navigate(["auth"]);
    }
  }

  onWriteAnEmail(){
    let dialog = this.dialog.open(DialogCreateMailComponent);
    dialog.componentInstance.onCreate = (receiverMail: string, mailText: string, mailSubject: string) => {
      let mailDto = new MailDTO(mailText, mailSubject, receiverMail, new Date());
      this.mailService.postMail({}, mailDto);
    }

    dialog.componentInstance.onClose = () => {
      dialog.close();
    }
  }

}
