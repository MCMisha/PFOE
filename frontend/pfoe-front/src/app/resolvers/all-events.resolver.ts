import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { EventService } from '../services/event.service';

@Injectable({
  providedIn: 'root'
})
export class AllEventsResolver implements Resolve<any> {
  constructor(private eventService: EventService) {}

  resolve(): Observable<any> {
    return this.eventService.getEvents();
  }
}
