import { AccessDeniedComponent } from './access-denied/access-denied.component';
import { AlertComponent } from './components/alert/alert.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor, ErrorInterceptor } from './common';
import { ComponentsModule } from './components/components.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';

import { MatButtonModule, MatSlideToggleModule, MatCheckboxModule,
  MAT_CHECKBOX_CLICK_ACTION, MatRippleModule, MatFormFieldModule,
   MatInputModule, MatSelectModule, MatTooltipModule } from '@angular/material';

import { NotificationsComponent } from './components/notifications/notifications.component';
import { LoginComponent } from './login/login.component';
import { AppRoutingModule } from './app-routing.module';


@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    HttpModule,
    ComponentsModule,
    ReactiveFormsModule,
    RouterModule,
    HttpClientModule,
    AppRoutingModule,
    MatButtonModule,
    MatRippleModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatTooltipModule,
    MatCheckboxModule,
    MatSlideToggleModule
  ],
  declarations: [
    AppComponent,
    LoginComponent,
    NotificationsComponent,
    AlertComponent,
    AccessDeniedComponent,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: MAT_CHECKBOX_CLICK_ACTION, useValue: 'check' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
