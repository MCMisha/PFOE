import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormControl} from "@angular/forms";
import {Subscription} from "rxjs";
import {UserService} from "./services/user.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'pfoe-front';
  menuItems = [
    {path: '', name: 'Strona główna'},
    {path: 'events', name: 'Wydarzenia'}
  ];
  search = new FormControl('');
  login: string = '';
  subscription = new Subscription();
  isLoggedIn: any = false;

  constructor(private userService: UserService, private router: Router) {
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
      if(this.search.value) {
        const query: string = this.search.value.trim();

        this.router.navigate(['/search'], {queryParams: {q: query}}).then( () => {
            this.search.setValue("");
          }
        );
      }
    }
  }

  logOut() {
    console.log(this.login);
  }
}
