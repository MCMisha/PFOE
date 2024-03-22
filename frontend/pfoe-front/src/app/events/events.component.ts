import {Component, OnInit} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {EventService} from "../services/event.service";
import {UserService} from "../services/user.service";
import {User} from "../models/user";

interface EventModelWithOrganizerName {
  id?: number;
  name?: string;
  location?: string;
  category?: string;
  date?: string;
  participantNumber?: number;
  organizer?: number;
  organizerName?: string;
  visitsNumber?: number;
  creation_date?: Date;
}

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrl: './events.component.scss'
})
export class EventsComponent implements OnInit {
  dataSource: MatTableDataSource<EventModelWithOrganizerName> = new MatTableDataSource<EventModelWithOrganizerName>();

  events: EventModelWithOrganizerName[] = []

  //   [
  //   {id: 1, name: 'Testowe wydarzenie', location: 'Testowe', category: 'Test', date: '2021-01-01', participant_number: 10, organizer: 1, visits_number: 0, creation_date: new Date()}, //placeholder dopoki backend nie dziala
  // ];

  users: User[] = [
    {id: 1, firstName: 'Testowy', lastName: 'Użytkownik'},  //placeholder dopoki backend nie dziala
    {id: 2, firstName: 'Adam', lastName: 'Nowak'}
  ];

  constructor(private eventService: EventService, private userService: UserService) {
  }

  ngOnInit(): void {
    this.loadEvents();
    //this.loadUsers();
  }

  loadEvents(): void {
    this.eventService.getEvents()
      .subscribe(events => {
        this.events = events as EventModelWithOrganizerName[];

        // Populate organizerName after events are loaded
        this.dataSource.data = this.events as EventModelWithOrganizerName[];

        this.dataSource.data.forEach(event => {
          const organizer = this.users.find(user => user.id === event.organizer);
          event.organizerName = organizer ? organizer.firstName + " " + organizer.lastName : 'Unknown';
        });
      });
  }


// loadUsers(): void {
//   this.userService.getUsers()
//     .subscribe((users: User[]) => {
//       this.users = users as User[];
//     });
// }


  columnsToDisplay = ['name', 'location', 'category', 'date', 'participantNumber', 'organizer'];
  selectedRow: EventModelWithOrganizerName | null = null;

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

  selectRow(row: EventModelWithOrganizerName): void {
    this.selectedRow = row;
  }
}
