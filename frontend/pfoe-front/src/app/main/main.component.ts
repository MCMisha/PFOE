import {Component, OnDestroy, OnInit} from '@angular/core';
import {EventService} from "../services/event.service";
import {Subscription} from "rxjs";
import {EventModel} from "../models/eventModel";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent implements OnInit, OnDestroy {

  private subscription = new Subscription();
  mostPopular: EventModel[] = [];
  newest: EventModel[] = [];
  constructor(private eventService: EventService) {
  }

  ngOnInit(): void {
    this.subscription.add(
      this.eventService.getMostPopular().subscribe(events => {
        this.mostPopular = events;
        console.log(this.mostPopular);
      })
    );
    this.subscription.add(
      this.eventService.getNewest().subscribe(events => {
        this.newest = events;
        console.log(this.newest);
      })
    );
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
