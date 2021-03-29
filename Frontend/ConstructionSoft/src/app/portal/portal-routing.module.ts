import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";
import {PortalComponent} from "./portal.component";
import {adminRoutes} from "./admin/admin.routing";
import {costEstimatorRoutes} from "./cost-estimator/cost-estimator.routing";
import {AuthRequiredGuard} from "../auth/guards/auth-required-guard.service";
import {DashboardComponent} from "./dashboard/dashboard.component";
import {AccountComponent} from "./account/account.component";

export const routes: Routes = [
  {
    path: 'portal',
    component: PortalComponent,
    children: [
      ...adminRoutes,
      ...costEstimatorRoutes,
      {path: '', redirectTo: 'dashboard', pathMatch: 'full'},
      {path: 'dashboard', component: DashboardComponent},
      {path: 'account', component: AccountComponent}
    ],
    canActivate: [AuthRequiredGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PortalRoutingModule {

}
