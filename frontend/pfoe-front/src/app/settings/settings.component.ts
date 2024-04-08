import {Component, OnInit} from '@angular/core';
import { SettingsService } from '../services/settings.service';
import {MatSnackBar} from "@angular/material/snack-bar";
import {Settings} from "../models/settings";

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.scss'
})

export class SettingsComponent implements OnInit { //todo: pobieranie userid zalogowanego uzytkownika
  selectedStyle: string | undefined;
  selectedFontSize: number | undefined;
  currentUserId: number = -1;

  constructor(private settingsService: SettingsService,
              private snackBar: MatSnackBar) {
  }

  ngOnInit() {
    if(this.currentUserId) {
      this.settingsService.getSettings(this.currentUserId).subscribe((settings) => {
        const {style, font_size} = settings as Settings;
        this.selectedStyle = style
        this.selectedFontSize = font_size
      });
    }
  }

  onSaveChangesClick() {
    if(this.selectedFontSize && this.selectedStyle) {
      this.settingsService.saveSettings(this.currentUserId, this.selectedStyle, this.selectedFontSize);
      console.log(this.selectedStyle);
      console.log(this.selectedFontSize);

      this.snackBar.open('Ustawienia zapisane.', '', {panelClass: 'snackbar'})._dismissAfter(3000);
    }
    else {
      this.snackBar.open('Nie wybrano ustawień!', '', {panelClass: 'snackbar'})._dismissAfter(1000);
    }
  }
}
