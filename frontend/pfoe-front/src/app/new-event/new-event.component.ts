import {Component, OnDestroy, OnInit} from '@angular/core';
import {EventService} from "../services/event.service";
import {ActivatedRoute, Event} from "@angular/router";
import {UserService} from "../services/user.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Subject, Subscription} from "rxjs";
import {AbstractControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {User} from "../models/user";
import {EventModel} from "../models/eventModel";
import {toNumbers} from "@angular/compiler-cli/src/version_helpers";


@Component({
  selector: 'app-new-event',
  templateUrl: './new-event.component.html',
  styleUrl: './new-event.component.scss'
})
export class NewEventComponent implements OnInit, OnDestroy{

  newEventForm = this.fb.group({
    eventName: ['', [Validators.required, Validators.maxLength(50)]],
    eventCategory: ['', [Validators.required]],
    eventDate: ['', [Validators.required]],
    eventMaxParticipants: ['', [Validators.required]],
    eventLocation: ['', [Validators.required, Validators.maxLength(50)]]
  });

  subscription: any = new Subscription();
  isLoading: boolean = false;

  constructor(
    private eventService: EventService,
    private userService: UserService,
    private snackBar: MatSnackBar,
    private fb: FormBuilder,) {
  }

  ngOnInit() {


  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  onCreateEventClick(){
    const event: EventModel = {
      name: this.newEventForm.value.eventName,
      category: this.newEventForm.value.eventCategory,
      date: this.newEventForm.value.eventDate,
      participantNumber: Number(this.newEventForm.value.eventMaxParticipants),
      location: this.newEventForm.value.eventLocation,
      visits_number: 0,
      creation_date: new Date()
    };

    this.isLoading = true;
    /*
    this.eventService.createEvent(event).subscribe(_ => {
      this.isLoading = false;
      this.snackBar.open('Udało się utworzyć wydarzenie', 'OK');
    });
    */
    this.isLoading = false;
    this.snackBar.open('Udało się utworzyć wydarzenie', 'OK');
    console.log(event.name, event.category, event.date, event.participantNumber, event.location);
    console.log(event.visits_number, event.creation_date)
  }

  disabledCreateEvent() {
    return this.newEventForm.invalid || this.isLoading;
  }

}

