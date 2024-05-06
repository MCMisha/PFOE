import {Component, OnDestroy, OnInit} from '@angular/core';
import {EventModel} from "../../models/eventModel";
import {ActivatedRoute} from "@angular/router";
import {catchError, map, of, Subscription, Subject, BehaviorSubject} from "rxjs";
import {EventService} from "../../services/event.service";
import {User} from "../../models/user";
import {UserService} from "../../services/user.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {takeUntil} from 'rxjs/operators';

@Component({
  selector: 'app-events',
  templateUrl: './eventsView.component.html',
  styleUrl: './eventsView.component.scss'
})
export class EventsViewComponent implements OnInit, OnDestroy {

  currentEvent: EventModel | undefined;
  private id: number = -1;
  private currentUserId: number | undefined = -1;
  public currentParticipants:  number = 0;
  private update = new BehaviorSubject<boolean>(false);
  login: string = '';
  isLoading: boolean = false;
  isLoggedIn: boolean = false;
  isSignedUp: boolean = false;
  isAnyPlaces: boolean = true;
  isButtonDisabled: boolean = false;
  private ngUnsubscribe = new Subject<void>();

  constructor(private eventService: EventService,
              private route: ActivatedRoute,
              private userService: UserService,
              private snackBar: MatSnackBar) {

  }

  async ngOnInit() {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.login = localStorage.getItem('login') || '';

    try {
      this.currentUserId = await this.userService.getIdByLogin(this.login).toPromise();
      this.currentParticipants = await this.eventService.getParticipantNumber(Number(this.id)).toPromise();
      this.isLoggedIn = Boolean(await this.userService.isLoggedIn(this.login).toPromise());
      this.isSignedUp = Boolean(await this.eventService.isUserSignedUpForEvent(this.currentUserId, this.id).toPromise());
      this.currentEvent = await this.eventService.getEvent(Number(this.id)).toPromise();
      this.isAnyPlaces = Boolean(this.currentParticipants < this.currentEvent?.participantNumber!);
    } catch (error) {
      console.error(error);
    }
    this.update.subscribe(() => {
      this.updateData();
    });

  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  onSignUpClick() {
    this.isLoading = true;
    this.eventService.addParticipant(this.currentUserId, this.id).subscribe(() => {
      this.snackBar.open('Udało się zapisać на wydarzenie', 'OK');
      this.isLoading = false;
      this.update.next(true);
    });
  }

  private async updateData() {
    this.currentParticipants = await this.eventService.getParticipantNumber(Number(this.id)).toPromise();
    this.isSignedUp = Boolean(await this.eventService.isUserSignedUpForEvent(this.currentUserId, this.id).toPromise());
    this.isButtonDisabled = !this.isLoggedIn || this.isSignedUp || this.isLoading || !this.isAnyPlaces;
  }
}
