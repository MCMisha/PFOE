import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { Observable } from 'rxjs';
import { EventService } from '../services/event.service';

@Injectable({
  providedIn: 'root'
})
export class MostPopularEventsResolver implements Resolve<any> {
  constructor(private eventService: EventService) {}

  resolve(): Observable<any> {
    return this.eventService.getMostPopular();
  }
}
