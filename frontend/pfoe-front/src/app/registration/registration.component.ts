import {Component, OnDestroy, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, Validators} from '@angular/forms';
import {UserService} from "../services/user.service";
import {MatSnackBar} from '@angular/material/snack-bar';
import {debounceTime, of, Subscription, switchMap} from "rxjs";
import {User} from "../models/user";

function formValidator(control: AbstractControl): { [key: string]: boolean } | null {
  const password = control.get('password');
  const repeatPassword = control.get('repeatPassword');

  if (password && repeatPassword && password.value !== repeatPassword.value) {
    return {'passwordMismatch': true};
  }

  return null;
}

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.scss'
})
export class RegistrationComponent implements OnInit, OnDestroy {
  registrationForm = this.fb.group({
    firstName: ['', [Validators.required, Validators.maxLength(30)]],
    lastName: ['', [Validators.required, Validators.maxLength(40)]],
    email: ['', [Validators.required,
      Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$'), Validators.maxLength(50)]],
    login: ['', [Validators.required, Validators.maxLength(50)]],
    password: ['', [Validators.required, Validators.maxLength(50)]],
    repeatPassword: ['', [Validators.required, Validators.maxLength(50)]]
  }, {
    validator: [formValidator]
  });

  existingEmailError = false;
  existingLoginError = false;
  matchingPasswordError = false;
  subscription: any = new Subscription();

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private userService: UserService) {
  }

  ngOnInit() {
    this.subscription.add(
      this.registrationForm.get('login')?.valueChanges.pipe(
        debounceTime(300),
        switchMap(login => {
            if (login === null || login === '') {
              return of(false);
            }
            return this.userService.checkLogin(login);
          }
        )
      ).subscribe(res => {
        this.existingLoginError = res as boolean;
      })
    );

    this.subscription.add(
      this.registrationForm.get('email')?.valueChanges.pipe(
        debounceTime(300),
        switchMap(email => {
          if (email === null || email === '') {
            return of(false);
          }
          return this.userService.checkEmail(email);
        })).subscribe(res => {
        this.existingEmailError = res as boolean;
      })
    );

    this.subscription.add(
      this.registrationForm.get('password')?.valueChanges.subscribe(password => {
        this.matchingPasswordError = password !== this.registrationForm.get('repeatPassword')?.value;
      })
    );

    this.subscription.add(
      this.registrationForm.get('repeatPassword')?.valueChanges.subscribe(repeatPassword => {
        this.matchingPasswordError = repeatPassword !== this.registrationForm.get('password')?.value;
      })
    );
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  onRegisterClick() {
    const user: User = {
      ...this.registrationForm.value
    };
    this.userService.register(user).subscribe(_ => {
      this.openSnackBar('Poczekaj na potwierdzenie e-mailem', 'OK');
    });
  }

  private openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action);
  }
}
