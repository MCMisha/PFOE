import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {catchError, Observable, of} from "rxjs";
import {EventModel} from "../models/eventModel";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(private http: HttpClient) { }

  search(searchTerm: string): Observable<EventModel[]> {
    return this.http.get<EventModel[]>(`${environment.baseApiUri}/Event/search/${searchTerm}`).pipe(
      catchError(this.handleError<EventModel[]>("search", []))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      this.log(`${operation} failed: ${error.message}`);
      return of(result as T)
    };
  }

  private log(message: string) {
    console.log(`SearchService: ${message}`);
  }
}
