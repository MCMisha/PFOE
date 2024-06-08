import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {EventsViewComponent} from "./eventsView.component";
import {EventResolver} from "../../resolvers/event.resolver";

const routes: Routes = [{ path: '', component: EventsViewComponent, resolve: {event: EventResolver} }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EventsViewRoutingModule { }
