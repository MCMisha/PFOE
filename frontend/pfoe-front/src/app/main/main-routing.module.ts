import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {MainComponent} from "./main.component";
import {NewEventComponent} from "../new-event/new-event.component";
import {ManageEventsComponent} from "../manage-events/manage-events.component";
import {authGuard} from "../guards/auth.guard";
import {EditEventComponent} from "../edit-event/edit-event.component";
import {NewestResolver} from "../resolvers/newest-events.resolver";
import {MostPopularEventsResolver} from "../resolvers/most-popular-events.resolver";

const routes: Routes = [
  {
    path: '', component: MainComponent, resolve: {
      mostPopular: MostPopularEventsResolver,
      newest: NewestResolver
    }
  },
  {path: 'event/new', component: NewEventComponent, canActivate: [authGuard]},
  {path: 'event/edit', component: EditEventComponent, canActivate: [authGuard]},
  {path: 'event/manage', component: ManageEventsComponent, canActivate: [authGuard]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule {
}
