import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {SearchComponent} from "./search/search.component";

const routes: Routes = [
  { path: '', loadChildren: () => import('./main/main.module').then(m => m.MainModule)},
  { path: 'events', loadChildren: () => import('./events/events.module').then(m => m.EventsModule) },
  { path: 'registration', loadChildren: () => import('./registration/registration.module').then(m => m.RegistrationModule) },
  { path: 'login', loadChildren: () => import('./login/login.module').then(m => m.LoginModule) },
  { path: 'search', component: SearchComponent}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
