import {Component, OnDestroy, OnInit} from '@angular/core';
import {EventService} from "../services/event.service";
import {of, Subscription, switchMap, takeUntil} from "rxjs";
import {EventModel} from "../models/eventModel";
import {UserService} from "../services/user.service";
import {BodyClassService} from "../services/body-class.service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent implements OnInit, OnDestroy {

  private subscription = new Subscription();
  mostPopular: EventModel[] = [];
  newest: EventModel[] = [];
  isLoggedIn: any = false;
  login: string = '';

  constructor(private eventService: EventService,
              private router: Router,
              private userService: UserService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.login = localStorage.getItem('login') || '';
    this.mostPopular = this.route.snapshot.data['mostPopular'];
    this.newest = this.route.snapshot.data['newest'];
    if (this.login !== '') {
      this.userService.isLoggedIn(this.login).subscribe(res => {
        this.isLoggedIn = Boolean(res);
      })
    }
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  onNewEventClick() {
    this.router.navigate(['/event/new']);
  }

  onManageEventsClick() {
    this.router.navigate(['/event/manage']);
  }
}
