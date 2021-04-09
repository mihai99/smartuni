import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/shared/services/auth.service';
import { NotificationsService } from 'src/app/shared/services/notifications.service';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registrationForm = new FormGroup({
    email: new FormControl('mihai@mailinator.com', [Validators.required, Validators.email]),
    firstName: new FormControl('test', [Validators.required]),
    lastName: new FormControl('test', [Validators.required]),
    numericCode: new FormControl('test', [Validators.required]),
    phone: new FormControl('test', [Validators.required]),
    password: new FormControl('Querty1234', [Validators.required]),
    confirmPassword: new FormControl('Querty1234', [Validators.required]),
  })

  public get email() { return this.registrationForm.get('email'); }
  public get password() { return this.registrationForm.get('password'); }
  public get firstName() { return this.registrationForm.get('firstName'); }
  public get lastName() { return this.registrationForm.get('lastName'); }
  public get numericCode() { return this.registrationForm.get('numericCode'); }
  public get phone() { return this.registrationForm.get('phone'); }
  constructor(public authService: AuthService, private _notifications: NotificationsService, private _rotuer: Router) { }

  ngOnInit(): void {
  }

  public SignUp() {
     this.authService.SignUp(this.email.value, this.password.value, this.firstName.value, this.lastName.value, this.phone.value, this.numericCode.value).then(result => {
      if (result) {
        this._notifications.ShowInfo("Account created, you can now login");
        this._rotuer.navigate(['']);
      } else {
        this._notifications.ShowError("Error creating your account, please try again")
      }
    });
  }
}
