import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {SettingsComponent} from "./settings/settings.component";
import {SearchComponent} from "./search/search.component";
import {authGuard} from "./guards/auth.guard";
import {noAuthGuard} from "./guards/no-auth.guard";


const routes: Routes = [
  { path: '', loadChildren: () => import('./main/main.module').then(m => m.MainModule)},
  { path: 'events', loadChildren: () => import('./events/events.module').then(m => m.EventsModule) },
  { path: 'registration', loadChildren: () => import('./registration/registration.module').then(m => m.RegistrationModule), canActivate: [noAuthGuard] },
  { path: 'login', loadChildren: () => import('./login/login.module').then(m => m.LoginModule), canActivate: [noAuthGuard] },
  { path: 'settings', component: SettingsComponent, canActivate: [authGuard]},
  { path: 'login', loadChildren: () => import('./login/login.module').then(m => m.LoginModule) },
  { path: 'search', component: SearchComponent},
  { path: 'event/:id', loadChildren: () => import('./events/eventsView/eventsView.module').then(m => m.EventsViewModule) }
];



@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
