import {Component, EventEmitter, OnDestroy, OnInit, Output} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {UserService} from "../services/user.service";
import {MatSnackBar} from '@angular/material/snack-bar';
import {catchError, map, of, Subscription} from "rxjs";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit, OnDestroy {
  @Output() loginSuccess = new EventEmitter();
  loginForm = this.fb.group({
    login: ['', [Validators.required, Validators.maxLength(50)]],
    password: ['', [Validators.required, Validators.maxLength(50)]],
  }, {});


  subscription: any = new Subscription();
  isLoading: boolean = false;

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
    this.isLoading = true;
    const _login = this.loginForm.value.login!;
    const _password = this.loginForm.value.password!;
    this.userService.login(_login, _password).pipe(
      map(userId => {
        this.userService.id = userId;
        this.userService.loginSuccess.next(_login);
        return _login;
      }),
      catchError((err) => {
        if (err.status === 404) {
          this.snackBar.open('Nieprawidłowy login lub hasło', 'OK');
        }
        return of(null);
      }),
    ).subscribe((res) => {
      this.isLoading = false;
      this.loginSuccess.emit(_login);
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
