import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormControl} from "@angular/forms";
import {of, Subject, Subscription, switchMap, takeUntil} from "rxjs";
import {UserService} from "./services/user.service";
import {Router} from "@angular/router";
import {BodyClassService} from "./services/body-class.service";
import {FontSize} from "./enums/font-size";
import {BgClass} from "./enums/bg-class";
import {SettingsService} from "./services/settings.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss', '../styles.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  menuItems = [
    {path: '', name: 'Strona główna'},
    {path: 'events', name: 'Wydarzenia'}
  ];
  fontSizeDefault = FontSize._16PX;
  pageStyleDefault = BgClass.LIGHT;
  search = new FormControl('');
  login: string = '';
  subscription = new Subscription();
  isLoggedIn: any = false;

  constructor(private userService: UserService,
              private settingsService: SettingsService,
              private bodyClassService: BodyClassService,
              private router: Router) {
  }

  ngOnInit() {
    this.login = localStorage.getItem('login') || '';
    if (this.login !== '') {
      this.userService.isLoggedIn(this.login).pipe(
        switchMap(res => {
          this.isLoggedIn = Boolean(res);
          if (!this.isLoggedIn) {
            this.bodyClassService.setStyles(this.pageStyleDefault, this.fontSizeDefault);
            return of(null);
          } else {
            return this.userService.getIdByLogin(this.login).pipe(
              switchMap(id => this.settingsService.getSettings(id))
            );
          }
        }),
        takeUntil(this.destroy$)
      ).subscribe(settings => {
        if (settings) {
          this.bodyClassService.setStyles(settings.style, settings.fontSize);
        }
      });
    }

    this.subscription.add(
      this.userService.loginSuccess.pipe(
        switchMap(login => {
          if (this.login === '') {
            this.login = login;
          }
          this.isLoggedIn = true;

          localStorage.setItem('login', login);
          return this.userService.isLoggedIn(this.login);
        }),
        switchMap(res => {
          this.isLoggedIn = Boolean(res);
          if (!this.isLoggedIn) {
            this.bodyClassService.setStyles(this.pageStyleDefault, this.fontSizeDefault);
            return of(null);
          } else {
            return this.userService.getIdByLogin(this.login).pipe(
              switchMap(id => this.settingsService.getSettings(id))
            );
          }
        }),
        takeUntil(this.destroy$)
      ).subscribe(settings => {
        if (settings) {
          this.bodyClassService.setStyles(settings.style, settings.fontSize);
        }
      })
    );
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
    this.subscription.unsubscribe();
  }

  onKeyDown($event: KeyboardEvent) {
    if ($event.key === 'Enter') {
      if (this.search.value) {
        const query: string = this.search.value.trim();

        this.router.navigate(['/search'], {queryParams: {q: query}}).then(() => {
            this.search.setValue("");
          }
        );
      }
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
