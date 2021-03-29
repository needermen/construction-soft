import {Routes} from "@angular/router";
import {AuthRequiredGuard} from "../../auth/guards/auth-required-guard.service";
import {TechnicsComponent} from "./technics/technics.component";
import {BuildingMaterialComponent} from "./material/building/building-material.component";
import {ConsumptionMaterialComponent} from "./material/consumption/consumption-material.component";
import {MainMaterialComponent} from "./material/main/main-material.component";
import {WorkersComponent} from "./hr/worker/workers.component";
import {BrigadesComponent} from "./hr/brigade/brigades.component";
import {OrganizationsComponent} from "./organizations/organizations.component";
import {UsersComponent} from "./users/users.component";
import {BuildingsComponent} from "../cost-estimator/buildings/buildings.component";

export const adminRoutes: Routes = [
  {
    path: 'admin', children: [
      {path: '', redirectTo: "orgs", pathMatch: 'full'},
      {path: 'technics', component: TechnicsComponent},
      {
        path: 'materials', children: [
          {path: '', redirectTo: 'building', pathMatch: 'full'},
          {path: 'building', component: BuildingMaterialComponent},
          {path: 'consumption', component: ConsumptionMaterialComponent},
          {path: 'main', component: MainMaterialComponent},
        ]
      },
      {
        path: 'hr', children: [
          {path: '', redirectTo: 'workers', pathMatch: 'full'},
          {path: 'workers', component: WorkersComponent},
          {path: 'brigades', component: BrigadesComponent},
        ]
      },
      {path: 'orgs', component: OrganizationsComponent},
      {path: 'users', component: UsersComponent},
      {path: 'objects', component: BuildingsComponent},
    ], canActivate: [AuthRequiredGuard]
  }
];
