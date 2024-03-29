import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {httpOptions} from "../constants/constants";
import {catchError, Observable, of} from "rxjs";
import {Settings} from "../settings";

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  constructor(private http: HttpClient) { }

  saveSettings(userId: number, selectedStyle: string, selectedFontSize: number) {
    const settings: Settings = {id: 0, user_id: userId, style: selectedStyle, font_size: selectedFontSize};
    return this.http.put(`${environment.baseApiUri}/Settings/edit`, settings, httpOptions).pipe(catchError(this.handleError('saveSettings', settings)));
  }

  createSettings(userId: number, selectedStyle: string, selectedFontSize: number) {
    const settings: Settings = {id: 0, user_id: userId, style: selectedStyle, font_size: selectedFontSize};
    return this.http.post(`${environment.baseApiUri}/Settings/new?`, settings, httpOptions).pipe(catchError(this.handleError('createSettings', settings)));
  }

  getSettings(id: number) {
    return this.http.get(`${environment.baseApiUri}/Settings/${id}`, httpOptions).pipe(catchError(this.handleError('getSettings', id)));
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
