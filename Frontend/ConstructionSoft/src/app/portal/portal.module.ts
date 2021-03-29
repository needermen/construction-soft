import {NgModule} from '@angular/core';
import {PortalComponent} from './portal.component';
import {AdminModule} from "./admin/admin.module";
import {CostEstimatorModule} from "./cost-estimator/cost-estimator.module";
import {SharedModule} from "../shared/shared.module";
import {BrowserModule} from "@angular/platform-browser";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {PortalRoutingModule} from "./portal-routing.module";
import { DashboardComponent } from './dashboard/dashboard.component';
import { ChangePasswordComponent } from './account/change-password/change-password.component';
import { AccountComponent } from './account/account.component';

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    SharedModule,

    AdminModule,
    CostEstimatorModule,

    PortalRoutingModule
  ],
  exports: [],
  declarations: [PortalComponent, DashboardComponent, ChangePasswordComponent, AccountComponent]
})
export class PortalModule {
}
