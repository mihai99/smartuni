import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/shared/services/auth.service';
import { NotificationsService } from 'src/app/shared/services/notifications.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm = new FormGroup({
    email: new FormControl('mihai99@mailinator.com', [Validators.required,Validators.email]),
    password: new FormControl('Querty1234', [Validators.required]),
  })

  public get email() { return this.loginForm.get('email'); }
  public get password() { return this.loginForm.get('password'); }

  constructor(
    private authService: AuthService,
    private router: Router,
    private notificationsService: NotificationsService) { }

  ngOnInit(): void {
  }

  public async sendloginRequest() {
    const signInResponse = await this.authService.SingIn(this.email.value, this.password.value);
    if(signInResponse) {
      this.notificationsService.ShowInfo("Welcome back");
      this.router.navigate(["/student"])
    }
  }
}
