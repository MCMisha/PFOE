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
    event_id: [0],
    eventName: ['', [Validators.required, Validators.maxLength(50)]],
    eventCategory: ['', [Validators.required]],
    eventDate: ['', [Validators.required]],
    eventMaxParticipants: ['', [Validators.required]],
    eventLocation: ['', [Validators.required, Validators.maxLength(50)]],
    eventOrganizer: [0],
    eventVisit_number: [0],
    creation_date: [new Date()]
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

      this.editEventForm.patchValue({
        event_id: this.event?.id,
        eventName: this.event?.name,
        eventCategory: this.event?.category,
        eventDate: this.event?.date,
        eventMaxParticipants: this.event?.participantNumber?.toString(), // Convert number to string
        eventLocation: this.event?.location,
        eventOrganizer: this.event?.organizer,
        eventVisit_number: this.event?.visits_number,
        creation_date: this.event?.creation_date,
      });

    }
    ngOnDestroy(): void {
      this.subscription.unsubscribe();
    }

  onEditEventClick(){

      /*
      this.event = {
        name: this.editEventForm.value.eventName,
        category: this.editEventForm.value.eventCategory,
        date: this.editEventForm.value.eventDate,
        participantNumber: Number(this.editEventForm.value.eventMaxParticipants),
        location: this.editEventForm.value.eventLocation,
      }
      */

    this.isLoading = true;
    this.eventService.updateEvent(this.editEventForm.value).subscribe(_ => {
      this.isLoading = false;
      this.snackBar.open('Udało się edytować wydarzenie', 'OK');
      this.router.navigate([''])
    });

  }

  disabledEditEvent() {
    return this.editEventForm.invalid || this.isLoading;
  }
}
