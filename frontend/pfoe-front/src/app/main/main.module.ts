import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { MainComponent } from './main.component';
import {MatCardModule} from "@angular/material/card";
import {MatButton} from "@angular/material/button";


@NgModule({
  declarations: [
    MainComponent
  ],
    imports: [
        MatCardModule,
        CommonModule,
        MainRoutingModule,
        MatButton
    ]
})
export class MainModule { }
