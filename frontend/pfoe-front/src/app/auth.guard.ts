import {Router} from '@angular/router';
import {inject} from "@angular/core";

import {UserService} from "./services/user.service";

export const authGuard = () => {
  const userSerivce = inject(UserService);
  const router = inject(Router);

  if (userSerivce._isLoggedIn) {
    return true;
  }

  return router.parseUrl('/events');
};
