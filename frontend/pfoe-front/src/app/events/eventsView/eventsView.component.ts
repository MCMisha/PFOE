import {Component, OnDestroy, OnInit} from '@angular/core';
import {EventModel} from "../../models/eventModel";
import {ActivatedRoute} from "@angular/router";
import {catchError, map, of, Subscription} from "rxjs";
import {EventService} from "../../services/event.service";
import {User} from "../../models/user";

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

    constructor(private eventService: EventService, private route: ActivatedRoute) {

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
    console.log("Zapisano do wydarzenia");
    console.log(this.currentEvent?.participantNumber);
  }


}
