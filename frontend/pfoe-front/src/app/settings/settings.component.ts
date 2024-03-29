import {Component, OnInit} from '@angular/core';
import { SettingsService } from '../services/settings.service';
import {MatSnackBar} from "@angular/material/snack-bar";
import {Settings} from "../models/settings";

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.scss'
})

export class SettingsComponent implements OnInit{
  selectedStyle: string = ''; // Property to hold the selected style
  selectedFontSize: string = 'medium' // Property to hold the selected font size
  currentUserId: number = 1; // Property to hold the current user id

  constructor(private settingsService: SettingsService,
              private snackBar: MatSnackBar) {
  }

  ngOnInit() {
    this.settingsService.getSettings(this.currentUserId).subscribe((settings) => {
      const { style, font_size} = settings as Settings;
      this.selectedStyle = style
      this.selectedFontSize = font_size.toString();
    });
  }

  onSaveChangesClick() {
    console.log('Selected Style:', this.selectedStyle);
    console.log('Selected Font Size:', this.selectedFontSize);

    this.settingsService.saveSettings(this.selectedStyle, parseInt(this.selectedFontSize)).subscribe(() => {
      this.snackBar.open('Ustawienia zapisane', 'OK');
    });
  }
}
