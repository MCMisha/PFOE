import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {EventService} from "../services/event.service";
import {UserService} from "../services/user.service";
import {User} from "../models/user";
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogTitle
} from '@angular/material/dialog';
import {MatButton} from "@angular/material/button";
import {MatPaginator} from "@angular/material/paginator";

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

  @ViewChild(MatPaginator) paginator!: MatPaginator

  constructor(private eventService: EventService, private userService: UserService, private dialog: MatDialog) {
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

        this.dataSource.paginator = this.paginator;
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
    if (this.selectedRow != null) {
      this.eventService.deleteEvent(this.selectedRow.id as number)
        .subscribe(() => {
          this.loadEvents();
          this.selectedRow = null;
        });
    }
    else {
      this.dialog.open(NoRowSelectedDialogComponent, {
        data: {
          width: '250px',
          message: 'Nie wybrano wydarzenia.'
        }
      });
    }
  }

  selectRow(row: EventModelWithOrganizerName): void {
    if (this.selectedRow == null) {
      this.selectedRow = row;
    }
    else {
      this.selectedRow = null;
    }
  }
}

@Component({
  selector: 'no-row-selected-dialog',
  template: `
    <h2 mat-dialog-title>Błąd</h2>
    <mat-dialog-content>
      <p>{{ data.message }}</p>
    </mat-dialog-content>
    <mat-dialog-actions style="justify-content: flex-end">
      <button mat-button [mat-dialog-close]="'OK'">OK</button>
    </mat-dialog-actions>
  `,
  imports: [
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
    MatDialogTitle,
    MatButton
  ],
  standalone: true
})
export class NoRowSelectedDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: { message: string }) { }
}
