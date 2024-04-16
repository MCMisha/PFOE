import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {MainComponent} from "./main.component";
import {EventsComponent} from "../events/events.component";
import {EventsViewComponent} from "../events/eventsView/eventsView.component";

const routes: Routes = [{ path: '', component: MainComponent },
                        { path: 'event', component: EventsComponent},
                        { path: 'event/:id', component: EventsViewComponent}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
