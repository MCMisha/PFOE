import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {EventModel} from "../models/eventModel";
import {FormBuilder, Validators} from "@angular/forms";
import {EventService} from "../services/event.service";
import {UserService} from "../services/user.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Subscription} from "rxjs";
import {eventCategoriesArray} from "../enums/event-category";

@Component({
  selector: 'app-edit-event',
  templateUrl: './edit-event.component.html',
  styleUrl: './edit-event.component.scss'
})
export class EditEventComponent implements OnInit, OnDestroy {
  event?: EventModel;

  editEventForm = this.fb.group({
    id: [0],
    name: ['', [Validators.required, Validators.maxLength(50)]],
    category: ['', [Validators.required]],
    date: ['', [Validators.required]],
    participantNumber: ['', [Validators.required]],
    location: ['', [Validators.required, Validators.maxLength(50)]],
    organizer: [0],
    visitsNumber: [0],
    creationDate: [new Date()]
  });

  subscription: any = new Subscription();
  isLoading: boolean = false;
  login: string = '';
  categories = eventCategoriesArray;
  todaysDate = new Date();

  constructor(
    private route: ActivatedRoute,
    private eventService: EventService,
    private snackBar: MatSnackBar,
    private fb: FormBuilder,
    private router: Router
  ) {

  }

  ngOnInit(): void {
    this.event = this.route.snapshot.data['event'];
    const creation_date = new Date(this.event?.creationDate?.toString().slice(0,23)!);

    this.editEventForm.patchValue({
      id: this.event?.id,
      name: this.event?.name,
      category: this.event?.category,
      date: this.event?.date,
      participantNumber: this.event?.participantNumber?.toString(), // Convert number to string
      location: this.event?.location,
      organizer: this.event?.organizer,
      visitsNumber: this.event?.visitsNumber,
      creationDate: creation_date,
    });

  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  onEditEventClick() {
    const eventModel: EventModel = {
      id: this.editEventForm.value.id!,
      name: this.editEventForm.value.name,
      category: this.editEventForm.value.category,
      date: this.editEventForm.value.date,
      participantNumber: Number(this.editEventForm.value.participantNumber),
      location: this.editEventForm.value.location,
      organizer: this.editEventForm.value.organizer,
      visitsNumber: this.editEventForm.value.visitsNumber,
      creationDate: this.editEventForm.value.creationDate,
    };

    this.isLoading = true;
    this.eventService.updateEvent(eventModel).subscribe(_ => {
      this.isLoading = false;
      this.snackBar.open('Udało się edytować wydarzenie', 'OK');
      this.router.navigate([''])
    });

  }

  disabledEditEvent() {
    return this.editEventForm.invalid || this.isLoading;
  }
}
