import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {httpOptions} from "../constants/constants";
import {catchError, Observable, of, switchMap} from "rxjs";
import {Settings} from "../models/settings";

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  constructor(private http: HttpClient) {
  }

  saveSettings(userId: number, selectedStyle: string, selectedFontSize: number): Observable<Settings> {
    return this.getSettings(userId).pipe(
      switchMap((res: Settings) => {
        const updatedSettings: Settings = {
          id: res.id,
          userId: userId,
          style: selectedStyle,
          fontSize: selectedFontSize
        };

        return this.http.put<Settings>(`${environment.baseApiUri}/Settings/edit`, updatedSettings, httpOptions)
          .pipe(
            catchError(this.handleError<Settings>('saveSettings'))
          );
      })
    );
  }

  getSettings(userId: number): Observable<Settings> {
    return this.http.get<Settings>(`${environment.baseApiUri}/Settings/${userId}`, httpOptions).pipe(catchError(this.handleError<Settings>('getSettings')));
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
