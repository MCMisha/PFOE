import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { MainComponent } from './main.component';
import {MatCardModule} from "@angular/material/card";


@NgModule({
  declarations: [
    MainComponent
  ],
  imports: [
    MatCardModule,
    CommonModule,
    MainRoutingModule
  ]
})
export class MainModule { }
