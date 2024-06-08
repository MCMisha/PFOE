import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {EventsComponent} from "./events.component";
import {EventsViewComponent} from "./eventsView/eventsView.component";

const routes: Routes = [{ path: '', component: EventsComponent },
                        { path: 'event/:id', component: EventsViewComponent}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EventsRoutingModule { }
