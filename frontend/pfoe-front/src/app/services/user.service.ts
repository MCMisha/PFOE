import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {catchError, Observable, of, Subject} from "rxjs";
import {httpOptions} from "../constants/constants";
import {User} from "../models/user";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  loginSuccess = new Subject<string>();
  _isLoggedIn: boolean = false;
  id: number | undefined;

  constructor(private http: HttpClient) {
  }

  checkLogin(login: string) {
    return this.http.get(`${environment.baseApiUri}/User/checkLogin?login=${login}`, httpOptions).pipe(catchError(this.handleError('checkLogin', login)));
  }

  checkEmail(email: string) {
    return this.http.get(`${environment.baseApiUri}/User/checkEmail?email=${email}`, httpOptions).pipe(catchError(this.handleError('checkEmail', email)));
  }

  register(user: User) {
    return this.http.post(`${environment.baseApiUri}/User/register`, user, httpOptions).pipe(catchError(this.handleError('register', user)));
  }

  login(login: string, password: string): Observable<number> {
    const encodedLogin = encodeURIComponent(login);
    const encodedPassword = encodeURIComponent(password);
    this.loginSuccess.next(login);
    return this.http.post<number>(`${environment.baseApiUri}/User/login?login=${encodedLogin}&password=${encodedPassword}`, httpOptions).pipe();
  }

  logOut(login: string) {
    const encodedLogin = encodeURIComponent(login);
    return this.http.delete(`${environment.baseApiUri}/User/logout?login=${encodedLogin}`, httpOptions).pipe();
  }

  isLoggedIn(login: string) {
    return this.http.get(`${environment.baseApiUri}/User/isLogged?login=${login}`, httpOptions).pipe(catchError(this.handleError('isLoggedIn', login)));
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
