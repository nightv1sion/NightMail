import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationComponent } from './components/authentication/authentication.component';
import { EditUserInfoComponent } from './components/edit-user-info/edit-user-info.component';
import { LoginComponent } from './components/login/login.component';
import { MailBoxComponent } from './components/mail-box/mail-box.component';
import { PasswordConfirmationComponent } from './components/password-confirmation/password-confirmation.component';
import { RegisterComponent } from './components/register/register.component';
import { UserManagementComponent } from './components/user-management/user-management.component';

const routes: Routes = [
  {
    path: 'auth', component: AuthenticationComponent, children: [
      { path: '', component: UserManagementComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: "user/edit", component: EditUserInfoComponent},
      { path: "user/passwordconfirmation", component: PasswordConfirmationComponent}
    ]
  },
  {
    path: "mailbox", component: MailBoxComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
