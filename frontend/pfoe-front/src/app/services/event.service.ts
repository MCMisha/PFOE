import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {EventModel} from "../models/eventModel";
import {catchError, Observable, of} from "rxjs";
import {environment} from "../../environments/environment";
import {httpOptions} from "../constants/constants";

@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private http: HttpClient) { }

  createEvent(event: EventModel)  {
    return this.http.post(`${environment.baseApiUri}/Event/new`, event, httpOptions).pipe(catchError(this.handleError('createEvent', event)));
  }

  getEvent(id: number) {
    return this.http.get(`${environment.baseApiUri}/Event/${id}`, httpOptions).pipe(catchError(this.handleError('getEvent', id)));
  }

  getEvents() {
    return this.http.get(`${environment.baseApiUri}/Event`, httpOptions).pipe(catchError(this.handleError('getEvents', [])));
  }
  updateEvent(event: EventModel) {
    return this.http.put(`${environment.baseApiUri}/Event/edit`, event, httpOptions).pipe(catchError(this.handleError('updateEvent', event)));
  }

  deleteEvent(id: number) {
    return this.http.delete(`${environment.baseApiUri}/Event/delete/${id}`, httpOptions).pipe(catchError(this.handleError('deleteEvent', id)));
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      console.error(error); // log to console instead

      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  private log(message: string) {
    console.log(`UserService: ${message}`);
  }
}
