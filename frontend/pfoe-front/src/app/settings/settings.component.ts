import {Component, OnInit} from '@angular/core';
import {SettingsService} from '../services/settings.service';
import {MatSnackBar} from "@angular/material/snack-bar";
import {Router} from "@angular/router";
import {UserService} from "../services/user.service";

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.scss'
})

export class SettingsComponent implements OnInit {
  selectedStyle: string | undefined;
  selectedFontSize: number | undefined;
  currentUserId: number | undefined;

  constructor(private router: Router,
              private settingsService: SettingsService,
              private snackBar: MatSnackBar,
              private userService: UserService) {
  }

  ngOnInit() {
    const login = localStorage.getItem('login');

    if (login) {
      this.userService.getIdByLogin(login).subscribe(id => {
        this.currentUserId = id;
      })
    }
    else {
      this.snackBar.open('Błąd pobierania użytkownika.')._dismissAfter(1000);
    }
  }

  onSaveChangesClick() {
    if (this.currentUserId) {
      if (this.selectedFontSize && this.selectedStyle) {
        this.settingsService.saveSettings(this.currentUserId, this.selectedStyle, this.selectedFontSize).subscribe(() => {
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
}
