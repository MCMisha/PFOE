import {Router} from '@angular/router';
import {inject} from "@angular/core";

import {UserService} from "./services/user.service";
import {catchError, map, of} from "rxjs";

export const authGuard = () => {
  const userService = inject(UserService);
  const router = inject(Router);

  const login = localStorage.getItem('login');

  return userService.isLoggedIn(login).pipe(
    map((res) => {
      if (res) {
        return true; // Allow navigation
      } else {
        return router.parseUrl('/login'); // Redirect to /login
      }
    }),
    catchError(() => {
      return of(router.parseUrl('/login')); // Redirect on error
    })
  );

};
