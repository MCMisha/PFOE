import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormControl} from "@angular/forms";
import {Subscription} from "rxjs";
import {UserService} from "./services/user.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'pfoe-front';
  menuItems = [
    {path: '', name: 'Strona główna'},
    {path: 'events', name: 'Wydarzenia'},
  ];
  search = new FormControl('');
  login: string = '';
  subscription = new Subscription();
  isLoggedIn: any = false;

  constructor(private userService: UserService) {
  }

  ngOnInit() {
    this.login = localStorage.getItem('login') || '';
    this.subscription.add(
      this.userService.loginSuccess.subscribe(login => {
        if(this.login === '') {
          this.login = login;
        }
        this.isLoggedIn = true;
        localStorage.setItem('login', login);
      })
    );
    this.subscription.add(
      this.userService.isLoggedIn(this.login).subscribe(res =>
      {
        this.isLoggedIn = Boolean(res);
      })
    )
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  onKeyDown($event: KeyboardEvent) {
    if ($event.key === 'Enter') {
      console.log(this.search.value);
    }
  }

  logOut() {
    this.subscription.add(this.userService.logOut(this.login).subscribe(_ => {
      this.isLoggedIn = false;
      this.login = '';
      window.location.reload();
    }));

  }
}
