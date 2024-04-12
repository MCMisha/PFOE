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

export class SettingsComponent implements OnInit { //todo: pobieranie userid zalogowanego uzytkownika
  selectedStyle: string | undefined;
  selectedFontSize: number | undefined;
  currentUserId: number | undefined;

  constructor(private router: Router,
              private settingsService: SettingsService,
              private snackBar: MatSnackBar,
              private userService: UserService) {
  }

  ngOnInit() {
    this.currentUserId = this.userService.id;
  }

  onSaveChangesClick() {
    if (this.selectedFontSize && this.selectedStyle && this.currentUserId) {
      this.settingsService.saveSettings(this.currentUserId, this.selectedStyle, this.selectedFontSize).subscribe(() => {
        this.router.navigate(['/']);
        this.snackBar.open('Ustawienia zapisane.', '', {panelClass: 'snackbar'})._dismissAfter(3000);
      })
    }
    else {
      this.snackBar.open('Nie wybrano ustawień!', '', {panelClass: 'snackbar'})._dismissAfter(1000);
    }
  }
}
