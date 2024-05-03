import {Component, OnDestroy, OnInit} from '@angular/core';
import {EventModel} from "../../models/eventModel";
import {ActivatedRoute} from "@angular/router";
import {catchError, map, of, Subscription} from "rxjs";
import {EventService} from "../../services/event.service";
import {User} from "../../models/user";
import {UserService} from "../../services/user.service";

@Component({
  selector: 'app-events',
  templateUrl: './eventsView.component.html',
  styleUrl: './eventsView.component.scss'
})
export class EventsViewComponent implements OnInit,  OnDestroy{

    private subscription = new Subscription();
    currentEvent: EventModel | undefined;
    private id: string | null | undefined | number;
    public currentParticipants: string | null | undefined | number;
    login: string = '';
    isLoggedIn: any = false;

    constructor(private eventService: EventService, private route: ActivatedRoute, private userService: UserService) {

    }

    ngOnInit() {
      this.id = this.route.snapshot.paramMap.get('id');

      this.subscription.add(
        this.eventService.getParticipantNumber(Number(this.id)).subscribe(participantCount => this.currentParticipants = participantCount)

      )

      const login = localStorage.getItem('login');

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
      this.login = localStorage.getItem('login') || '';
      this.subscription.add(
        this.userService.isLoggedIn(this.login).subscribe(res => {
          this.isLoggedIn = Boolean(res);
        })
      )



      this.eventService.addParticipant(2, Number(this.id)).subscribe();

    //console.log("Zapisano do wydarzenia");
    //console.log(this.currentEvent?.participantNumber);
  }


}
