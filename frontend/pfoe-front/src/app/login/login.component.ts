import {Component, OnDestroy, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, Validators} from '@angular/forms';
import {UserService} from "../services/user.service";
import {MatSnackBar} from '@angular/material/snack-bar';
import {catchError, debounceTime, map, of, Subscription, switchMap, throwError} from "rxjs";
import {User} from "../models/user";
import {HttpErrorResponse} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit, OnDestroy {
  loginForm = this.fb.group({
    login: ['', [Validators.required, Validators.maxLength(50)]],
    password: ['', [Validators.required, Validators.maxLength(50)]],
  }, {});


  subscription: any = new Subscription();

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private userService: UserService,
    private router: Router) {
  }

  ngOnInit() {


  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  onLoginClick() {
    const _login = this.loginForm.value.login!;
    const _password = this.loginForm.value.password!;

    this.userService.login(_login, _password).pipe(
      map(_ => _login),
    catchError((err) => {
      if (err.status === 404) {
        this.snackBar.open('Nieprawidłowy login lub hasło', 'OK');
      }
      return of(null);
    }),
  ).
    subscribe((res) => {
      if (res !== null) {
        this.openSnackBar('Udało się zalogować', 'OK');
        this.router.navigate(['..']);
      }
    });
  }

  private openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action);
  }

}
