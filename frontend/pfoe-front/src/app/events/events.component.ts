import { Component } from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {EventModel} from "../models/eventModel";

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrl: './events.component.scss'
})
export class EventsComponent {
  dataSource = new MatTableDataSource<EventModel>([
    {
      id: 1,
      name: 'Event 1',
      location: 'Location 1',
      category: 'Category 1',
      date: '2024-03-17',
      participant_number: 100,
      organizer: 1,
      visits_number: 50,
      creation_date: new Date()
    },
    {
      id: 2,
      name: 'Event 2',
      location: 'Location 2',
      category: 'Category 2',
      date: '2024-03-18',
      participant_number: 150,
      organizer: 2,
      visits_number: 60,
      creation_date: new Date()
    }
  ]);

  columnsToDisplay = ['name', 'location', 'category', 'date', 'participant_number', 'organizer'];

  handleNew() {
    console.log('New button clicked');
    // Add your logic for the "New" button here
  }

  handleEdit() {
    console.log('Edit button clicked');
    // Add your logic for the "Edit" button here
  }

  handleDelete() {
    console.log('Delete button clicked');
    // Add your logic for the "Delete" button here
  }
}
