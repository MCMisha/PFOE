import {Component, OnDestroy, OnInit} from '@angular/core';
import {SettingsService} from '../services/settings.service';
import {MatSnackBar} from "@angular/material/snack-bar";
import {Router} from "@angular/router";
import {UserService} from "../services/user.service";
import {BodyClassService} from "../services/body-class.service";
import {Subject, Subscription, switchMap, takeUntil} from "rxjs";
import {FontSize} from "../enums/font-size";

// @ts-ignore
@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.scss'
})

export class SettingsComponent implements OnInit, OnDestroy {
  selectedStyle: string | undefined;
  selectedFontSize: number | undefined;
  currentUserId: number | undefined;

  subscription = new Subscription();
  private destroy$ = new Subject<void>();
  constructor(private router: Router,
              private settingsService: SettingsService,
              private bodyClassService: BodyClassService,
              private snackBar: MatSnackBar,
              private userService: UserService) {
  }

  ngOnInit() {
    const login = localStorage.getItem('login');

    if (login) {
      this.userService.getIdByLogin(login).pipe(
        switchMap(id => {
          this.currentUserId = id;
          return this.settingsService.getSettings(this.currentUserId);
        }),
        takeUntil(this.destroy$)
      ).subscribe(settings => {
        this.selectedStyle = settings.style;
        this.selectedFontSize = settings.fontSize;
        this.bodyClassService.setStyles(this.selectedStyle, this.selectedFontSize);
      });
    }
    else {
      this.snackBar.open('Błąd pobierania użytkownika.')._dismissAfter(1000);
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
    this.subscription.unsubscribe();
  }

  onSaveChangesClick() {
    if (this.currentUserId) {
      if (this.selectedFontSize && this.selectedStyle) {
        this.settingsService.saveSettings(this.currentUserId, this.selectedStyle, this.selectedFontSize).subscribe(() => {
          this.bodyClassService.setStyles(this.selectedStyle, this.selectedFontSize);
          void this.router.navigate(['/']);
          this.snackBar.open('Ustawienia zapisane.')._dismissAfter(3000);
        })
      }
      else {
        this.snackBar.open('Nie wybrano ustawień!')._dismissAfter(1000);
      }
    }
    else {
      this.snackBar.open('Błąd pobierania użytkownika.')._dismissAfter(1000);
    }
  }

  protected readonly FontSize = FontSize;
}
