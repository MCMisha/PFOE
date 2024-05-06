import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {EventsViewComponent} from "./eventsView.component";

const routes: Routes = [{ path: '', component: EventsViewComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EventsViewRoutingModule { }
