import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatButtonModule, MatRippleModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatTooltipModule } from '@angular/material';
import { HomeComponent } from './home/home.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    MatButtonModule,
    MatRippleModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatTooltipModule,
  ],
  declarations: [
    HomeComponent,
  ],
  exports: [
  ]
})
export class ComponentsModule { }
