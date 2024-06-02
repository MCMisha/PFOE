import {Component, OnDestroy, OnInit} from '@angular/core';
import {EventService} from "../services/event.service";
import {ActivatedRoute, Event, Router} from "@angular/router";
import {UserService} from "../services/user.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Subject, Subscription, switchMap, takeUntil} from "rxjs";
import {AbstractControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {User} from "../models/user";
import {EventModel} from "../models/eventModel";
import {toNumbers} from "@angular/compiler-cli/src/version_helpers";
import {EventCategory, eventCategoriesArray} from "../enums/event-category";
import {FontSize} from "../enums/font-size";


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
  private currentUserId: number | undefined = -1;
  login: string = '';
  categories = eventCategoriesArray;
  todaysDate = new Date();

  constructor(
    private eventService: EventService,
    private userService: UserService,
    private snackBar: MatSnackBar,
    private fb: FormBuilder,
    private router: Router
    ) {
  }

  async ngOnInit() {
    this.login = localStorage.getItem('login') || '';

    try {
      this.currentUserId = await this.userService.getIdByLogin(this.login).toPromise();
    } catch (error) {
      console.error(error);
    }

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
      organizer: this.currentUserId,
      visitsNumber: 0,
      creationDate: new Date()
    };

    this.isLoading = true;
    this.eventService.createEvent(event).subscribe(_ => {
      this.isLoading = false;
      this.snackBar.open('Udało się utworzyć wydarzenie', 'OK');
      this.router.navigate([''])
    });

  }

  disabledCreateEvent() {
    return this.newEventForm.invalid || this.isLoading;
  }

}

