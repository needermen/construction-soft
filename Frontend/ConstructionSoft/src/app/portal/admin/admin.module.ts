import {NgModule} from '@angular/core';
import {SharedModule} from "../../shared/shared.module";

//technic
import { TechnicsComponent } from './technics/technics.component';
import { TechnicCategoriesComponent } from './technics/categories/technic-categories.component';
import {TechnicDimensionsComponent} from "./technics/dimensions/technic-dimensions.component";

import {TechnicCategoryService} from "./technics/services/technic-category.service";
import {TechnicDimensionService} from "./technics/services/technic-dimension.service";
import {TechnicService} from "./technics/services/technic.service";

//materials
import { MaterialDimensionComponent } from './material/dimension/material-dimension.component';
import { BuildingMaterialComponent} from './material/building/building-material.component';
import { BuildingMaterialCategoryComponent } from "./material/building/category/building-material-category.component";
import { ConsumptionMaterialComponent } from './material/consumption/consumption-material.component';
import { ConsumptionMaterialCategoryComponent } from './material/consumption/category/consumption-material-category.component';
import { MainMaterialComponent } from './material/main/main-material.component';
import { MainMaterialCategoryComponent } from './material/main/category/main-material-category.component';

import {MaterialDimensionService} from "./material/services/material-dimension.service";
import {BuildingMaterialCategoryService} from "./material/services/building-material-category.service";
import {BuildingMaterialService} from "./material/services/building-material.service";
import {ConsumptionMaterialService} from "./material/services/consumption-material.service";
import {ConsumptionMaterialCategoryService} from "./material/services/consumption-material-category.service";
import {MainMaterialService} from "./material/services/main-material.service";
import {MainMaterialCategoryService} from "./material/services/main-material-category.service";

//hr
import {PaymentTypeService} from "./hr/services/paymentType.service";
import {WorkerCategoryService} from "./hr/services/worker-category.service";
import {WorkerService} from "./hr/services/worker.service";
import {BrigadeCategoryService} from "./hr/services/brigade-category.service";
import {BrigadeService} from "./hr/services/brigade.service";

import {PaymentTypesComponent} from "./hr/paymentType/paymentTypes.component";
import {WorkerCategoriesComponent} from "./hr/worker/categories/worker-categories.component";
import {WorkersComponent} from "./hr/worker/workers.component";
import {BrigadeCategoriesComponent} from "./hr/brigade/categories/brigade-categories.component";
import {BrigadesComponent} from "./hr/brigade/brigades.component";

//org
import { OrganizationsComponent } from './organizations/organizations.component';
import {OrganizationService} from "./organizations/services/organization.service";
import {UserService} from "./users/services/user.service";
import {UsersComponent} from "./users/users.component";
import {RoleService} from "./roles/services/role.service";
import {UserRoleService} from "./users/services/user-role.service";


@NgModule({
  declarations: [
    //technics
    TechnicDimensionsComponent,
    TechnicCategoriesComponent,
    TechnicsComponent,

    //materials
    MaterialDimensionComponent,
    BuildingMaterialComponent,
    BuildingMaterialCategoryComponent,
    ConsumptionMaterialComponent,
    ConsumptionMaterialCategoryComponent,
    MainMaterialComponent,
    MainMaterialCategoryComponent,

    //hr
    PaymentTypesComponent,
    WorkerCategoriesComponent,
    WorkersComponent,
    BrigadeCategoriesComponent,
    BrigadesComponent,

    //org
    OrganizationsComponent,
    UsersComponent
  ],
  imports: [
    SharedModule
  ],
  providers: [
    //technic
    TechnicCategoryService,
    TechnicDimensionService,
    TechnicService,

    //materials
    MaterialDimensionService,
    BuildingMaterialCategoryService,
    BuildingMaterialService,
    ConsumptionMaterialService,
    ConsumptionMaterialCategoryService,
    MainMaterialService,
    MainMaterialCategoryService,

    //hr
    PaymentTypeService,
    WorkerCategoryService,
    WorkerService,
    BrigadeCategoryService,
    BrigadeService,

    //org
    OrganizationService,
    UserService,
    RoleService,
    UserRoleService
  ]
})
export class AdminModule{

}
