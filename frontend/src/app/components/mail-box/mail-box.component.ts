import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-mail-box',
  templateUrl: './mail-box.component.html',
  styleUrls: ['./mail-box.component.css']
})
export class MailBoxComponent implements OnInit {

  constructor(private authService: AuthenticationService, private router: Router) {
    
   }

  ngOnInit(): void {
    if(!this.authService.isAuthenticated()){
      this.router.navigate(["auth"]);
    }
  }

}
