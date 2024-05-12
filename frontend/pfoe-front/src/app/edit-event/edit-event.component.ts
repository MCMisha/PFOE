import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {EventModel} from "../models/eventModel";

@Component({
  selector: 'app-edit-event',
  templateUrl: './edit-event.component.html',
  styleUrl: './edit-event.component.scss'
})
export class EditEventComponent implements OnInit, OnDestroy {
    event?: EventModel;
    constructor(private route: ActivatedRoute) {
    }

    ngOnInit(): void {
      this.event = this.route.snapshot.data['event'];
    }
    ngOnDestroy(): void {
    }
}
