import {Component, OnDestroy, OnInit} from '@angular/core';
import {EventModel} from "../models/eventModel";
import {Subscription} from "rxjs";
import {EventService} from "../services/event.service";

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrl: './events.component.scss'
})
export class EventsComponent implements OnInit, OnDestroy{

  private subscription = new Subscription();
  tempEvent: EventModel[] = [];

  constructor(private eventService: EventService) {
  }

  ngOnInit() {
    this.subscription.add(
      this.eventService.getNewest().subscribe(events => {
        this.tempEvent = events;
      })
    );
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
