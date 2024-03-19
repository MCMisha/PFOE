import {Component, OnDestroy, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, Validators} from '@angular/forms';
import {UserService} from "../services/user.service";
import {MatSnackBar} from '@angular/material/snack-bar';
import {catchError, debounceTime, of, Subscription, switchMap} from "rxjs";
import {User} from "../models/user";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit, OnDestroy{
  loginForm = this.fb.group({
    login: ['', [Validators.required, Validators.maxLength(50)]],
    password: ['', [Validators.required, Validators.maxLength(50)]],
  }, {

  });


  subscription: any = new Subscription();

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private userService: UserService) {
  }

  ngOnInit() {


  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  onLoginClick() {
    const _login: string = this.loginForm.value.login!;
    const _password: string = this.loginForm.value.password!;

    this.userService.login(_login, _password).subscribe(res => {
      console.log(res);
      this.openSnackBar('Udało się zalogować', 'OK');
    }, error => {
      this.openSnackBar(error.error.message, 'OK');
    });
  }

  private openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action);
  }
}
