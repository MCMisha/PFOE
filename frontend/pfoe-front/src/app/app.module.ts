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
@NgModule({
  declarations: [
    AppComponent,
    SettingsComponent,
    AppComponent,
    SearchComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
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
    MatTable,
    MatHeaderCell,
    MatCellDef,
    MatColumnDef,
    MatCell,
    MatCell,
    MatCell,
    MatCell,
    MatRowDef,
    MatHeaderCellDef,
    MatHeaderRowDef,
    MatHeaderRow,
    MatRow,
    MatPaginator
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
