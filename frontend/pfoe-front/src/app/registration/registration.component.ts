import { Component } from '@angular/core';
import {FormBuilder, Validators } from '@angular/forms';
@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.scss'
})
export class RegistrationComponent {
  registrationForm = this.fb.group({
    firstName: ['', [Validators.required, Validators.maxLength(30)]],
    lastName: ['', [Validators.required, Validators.maxLength(40)]],
    email: ['', [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$'), Validators.maxLength(50)]],
    login: ['', [Validators.required, Validators.maxLength(50)]],
    password: ['', [Validators.required, Validators.maxLength(50)]],
    repeatPassword: ['', [Validators.required, Validators.maxLength(50)]]
  });
  constructor(private fb: FormBuilder) {
  }

  onRegisterClick() {
    console.log(this.registrationForm.value);
  }
}
