import {Component, OnDestroy, OnInit} from '@angular/core';
import {EventModel} from "../../models/eventModel";
import {ActivatedRoute} from "@angular/router";
import {catchError, map, of, Subscription} from "rxjs";
import {EventService} from "../../services/event.service";
import {User} from "../../models/user";
import {UserService} from "../../services/user.service";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-events',
  templateUrl: './eventsView.component.html',
  styleUrl: './eventsView.component.scss'
})
export class EventsViewComponent implements OnInit,  OnDestroy{

    private subscription = new Subscription();
    currentEvent: EventModel | undefined;
    private id: string | null | undefined | number;
    private currentUserId: null | undefined | number;
    public currentParticipants: string | null | undefined | number;
    login: string = '';
    isLoggedIn: any = false;
    isSignedUp: any = false;


    constructor(private eventService: EventService, private route: ActivatedRoute, private userService: UserService, private snackBar: MatSnackBar) {

    }

    ngOnInit() {
      this.id = this.route.snapshot.paramMap.get('id');

      this.login = localStorage.getItem('login') || '';
      this.subscription.add(this.userService.getIdByLogin(this.login).subscribe(res => {
        this.currentUserId = res;
        })
      )

      this.subscription.add(
        this.eventService.getParticipantNumber(Number(this.id)).subscribe(participantCount => this.currentParticipants = participantCount)
      )

      this.login = localStorage.getItem('login') || '';
      this.subscription.add(
        this.userService.isLoggedIn(this.login).subscribe(res => {
          this.isLoggedIn = Boolean(res);
        })
      )

      this.subscription.add(
        this.eventService.isUserSignedUpForEvent(this.currentUserId, this.id).subscribe(res => {
          this.isSignedUp = Boolean(res);
        })
      )

      this.subscription.add(
        this.eventService.getEvent(Number(this.id)).subscribe(event => {
          this.currentEvent = event;
        })
      );
    }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  onSignUpClick() {


      if(this.currentEvent?.id === undefined){
        return;
      }
      this.eventService.addParticipant(Number(this.id), this.currentEvent?.id).subscribe();

      this.snackBar.open('Udało się zapisać na wydarzenie', 'OK');
      window.location.reload();
  }

  private openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action);
  }


}
