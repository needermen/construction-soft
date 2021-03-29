import {NgModule} from '@angular/core';

import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';

import {AppRoutingModule} from "../app-routing.module";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ResponseInterceptor} from "../shared/interceptors/respone.interceptor";
import {MessageService} from "primeng/api";
import {AuthRequestInteceptor} from "../auth/services/auth-request.inteceptor";
import {AuthResponseInterceptor} from "../auth/services/auth-response.interceptor";
import {FullPageLoadingService} from "../shared/services/fullPageLoading.service";

@NgModule({
  imports: [
    HttpClientModule,
    BrowserAnimationsModule,

    AppRoutingModule
  ],
  exports: [
    AppRoutingModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthRequestInteceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthResponseInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ResponseInterceptor, multi: true },

    MessageService,
    FullPageLoadingService
  ]
})
export class CoreModule {

}
