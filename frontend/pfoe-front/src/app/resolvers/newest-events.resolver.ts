import {Injectable} from "@angular/core";
import {Resolve} from "@angular/router";
import {EventService} from "../services/event.service";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class NewestResolver implements Resolve<any> {
  constructor(private eventService: EventService) {}

  resolve(): Observable<any> {
    return this.eventService.getNewest();
  }
}
