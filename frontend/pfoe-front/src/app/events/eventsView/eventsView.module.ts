import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EventsViewRoutingModule } from './eventsView-routing.module';
import { EventsViewComponent } from './eventsView.component';
import {MatButton} from "@angular/material/button";
import {MatCard, MatCardContent, MatCardHeader} from "@angular/material/card";
import {MatError, MatFormField, MatLabel} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {MatProgressSpinner} from "@angular/material/progress-spinner";
import {ReactiveFormsModule} from "@angular/forms";


@NgModule({
  declarations: [
    EventsViewComponent
  ],
  imports: [
    CommonModule,
    EventsViewRoutingModule,
    MatButton,
    MatCard,
    MatCardContent,
    MatCardHeader,
    MatError,
    MatFormField,
    MatInput,
    MatLabel,
    MatProgressSpinner,
    ReactiveFormsModule
  ]
})
export class EventsViewModule { }
