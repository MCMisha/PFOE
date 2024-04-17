import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {MatPaginator} from "@angular/material/paginator";
import {EventService} from "../services/event.service";
import {UserService} from "../services/user.service";
import {MatDialog} from "@angular/material/dialog";
import {NoRowSelectedDialogComponent} from "../events/events.component";
import {EventModel} from "../models/eventModel";
import {Subject, Subscription, switchMap, takeUntil} from "rxjs";
import {Router} from "@angular/router";

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
  selector: 'app-manage-events',
  templateUrl: './manage-events.component.html',
  styleUrl: './manage-events.component.scss'
})
export class ManageEventsComponent implements OnInit, OnDestroy {
  dataSource: MatTableDataSource<EventModel> = new MatTableDataSource<EventModel>();
  events: EventModel[] = [];
  currentUserId: number | undefined;
  private subscription = new Subscription();
  @ViewChild(MatPaginator) paginator!: MatPaginator
  private destroy$ = new Subject<void>();

  constructor(private eventService: EventService,
              private userService: UserService,
              private router: Router,
              private dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.loadEvents();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
    this.subscription.unsubscribe();
  }

  loadEvents(): void {
    const login = localStorage.getItem('login');
    if (login) {
      this.subscription.add(
        this.userService.getIdByLogin(login).pipe(
          switchMap(id => {
            this.currentUserId = id;
            return this.eventService.getEventsByOrganizerId(this.currentUserId);
          }),
          takeUntil(this.destroy$)
        ).subscribe(events => {
          this.events = events as EventModel[];
          this.dataSource.data = this.events;
          this.dataSource.paginator = this.paginator;
        })
      );
    }

  }

  columnsToDisplay = ['name', 'location', 'category', 'date', 'participantNumber'];
  selectedRow: EventModelWithOrganizerName | null = null;

  handleNew() {
    // Add your logic for the "New" button here
  }

  handleEdit() {
    this.router.navigate(['/event/edit'], { queryParams:{id: this.selectedRow?.id}});
  }

  handleDelete() {
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
    this.selectedRow = row;
  }

  loseFocus() {
    this.selectedRow = null;
  }
}
