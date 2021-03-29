import {NgModule} from '@angular/core';

import {AppComponent} from './app.component';

import {CoreModule} from "./core/core.module";
import {AuthModule} from "./auth/auth.module";

import {BrowserModule} from "@angular/platform-browser";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {SharedModule} from "./shared/shared.module";
import {PortalModule} from "./portal/portal.module";

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,

    CoreModule,
    AuthModule,
    PortalModule,
    SharedModule
  ],
  declarations: [
    AppComponent,
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
