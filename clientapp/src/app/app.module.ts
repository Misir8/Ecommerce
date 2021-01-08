import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {NgxSpinnerModule} from 'ngx-spinner';

import { AppComponent } from './app.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {CoreModule} from './core/core.module';
import {AppRoutingModule} from './app-routing.module';
import {RouterModule} from '@angular/router';
import {ErrorInterceptor} from './core/interceptor/error.interceptor';
import {LoadingInterceptors} from './core/interceptor/loading.interceptors';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {HomeModule} from './home/home.module';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    NgbModule,
    HttpClientModule,
    CoreModule,
    AppRoutingModule,
    RouterModule,
    NgxSpinnerModule,
    BrowserAnimationsModule,
    HomeModule,
    ToastrModule.forRoot()
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptors, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
