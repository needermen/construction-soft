import {Routes} from "@angular/router";
import {BuildingsComponent} from "./buildings/buildings.component";
import {AuthRequiredGuard} from "../../auth/guards/auth-required-guard.service";
import {BudgetComponent} from "./budget/budget.component";

export const costEstimatorRoutes: Routes = [
  {
    path: 'cost-estimator', children: [
      {path: '', redirectTo: 'buildings', pathMatch: 'full'},
      {path: 'buildings', component: BuildingsComponent},
      {
        path: 'budget/:id', component: BudgetComponent
      }
    ],
    canActivate: [AuthRequiredGuard]
  }
];
