import { Component } from '@angular/core';
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'pfoe-front';
  menuItems = [
    {path: '', name: 'Strona główna'},
    {path: 'events', name: 'Wydarzenia'}
  ];
  search = new FormControl('');

  onKeyDown($event: KeyboardEvent) {
    if ($event.key === 'Enter') {
      console.log(this.search.value);
    }
  }
}
