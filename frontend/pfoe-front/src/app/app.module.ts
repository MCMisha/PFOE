import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import {MatMenuModule} from '@angular/material/menu';
import {MatButtonModule} from '@angular/material/button';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { SearchComponent } from './search/search.component';
import {
  MatCell,
  MatCellDef,
  MatColumnDef,
  MatHeaderCell,
  MatHeaderCellDef, MatHeaderRow, MatHeaderRowDef, MatRow,
  MatRowDef,
  MatTable
} from "@angular/material/table";
import {MatPaginator} from "@angular/material/paginator";
import { SettingsComponent } from './settings/settings.component';
import {MatOption, MatSelect} from "@angular/material/select";
import { NewEventComponent } from './new-event/new-event.component';
import { ManageEventsComponent } from './manage-events/manage-events.component';
import { EditEventComponent } from './edit-event/edit-event.component';
import {RouterModule} from "@angular/router";
import {MatCard, MatCardContent, MatCardHeader} from "@angular/material/card";
import {MatProgressSpinner} from "@angular/material/progress-spinner";
import {
  MatDatepicker,
  MatDatepickerInput,
  MatDatepickerModule,
  MatDatepickerToggle
} from "@angular/material/datepicker";
import {MAT_DATE_LOCALE, MatNativeDateModule} from "@angular/material/core";
import * as moment from 'moment';

@NgModule({
  declarations: [
    AppComponent,
    SettingsComponent,
    AppComponent,
    SearchComponent,
    NewEventComponent,
    ManageEventsComponent,
    EditEventComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    ReactiveFormsModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    MatMenuModule,
    AppRoutingModule,
    HttpClientModule,
    MatSelect,
    MatOption,
    FormsModule,
    HttpClientModule,
    RouterModule,
    MatTable,
    MatHeaderCell,
    MatCellDef,
    MatColumnDef,
    MatCell,
    MatRowDef,
    MatHeaderCellDef,
    MatHeaderRowDef,
    MatHeaderRow,
    MatRow,
    MatPaginator,
    MatCardContent,
    MatCard,
    MatCardHeader,
    MatProgressSpinner,
    MatDatepicker,
    MatDatepickerToggle,
    MatDatepickerInput
  ],
  providers: [
    {provide: MAT_DATE_LOCALE, useValue: 'pl-PL'},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
