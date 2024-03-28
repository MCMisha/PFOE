import { Component } from '@angular/core';
import { SettingsService } from '../services/settings.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.scss'
})

export class SettingsComponent {
  selectedStyle: string; // Property to hold the selected style
  selectedFontSize: string; // Property to hold the selected font size

  constructor(private settingsService: SettingsService) {
    // Initialize the properties with default values if needed
    this.selectedStyle = '';
    this.selectedFontSize = '';
  }

  // Method to handle saving changes
  saveChanges() {
    // Here you can save the selected values to your database or perform any other action
    console.log('Selected Style:', this.selectedStyle);
    console.log('Selected Font Size:', this.selectedFontSize);

    // Call the service to save the settings
    this.settingsService.saveSettings(this.selectedStyle, this.selectedFontSize).subscribe((response) => {
      console.log('Settings saved successfully:', response);
    });
  }
}
