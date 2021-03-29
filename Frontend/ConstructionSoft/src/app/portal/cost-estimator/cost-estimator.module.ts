import {NgModule} from "@angular/core";
import {SharedModule} from "../../shared/shared.module";

import {BuildingsComponent} from "./buildings/buildings.component";
import {PhaseComponent} from './buildings/phase/phase.component';
import {WorkCategoryComponent } from './buildings/phase/work-category/work-category.component';
import {WorkComponent} from "./buildings/phase/work-category/work/work.component";
import {WorkBuildingMaterialComponent} from './buildings/phase/work-category/work/resources/work-building-material/work-building-material.component';
import {WorkConsumptionMaterialComponent} from "./buildings/phase/work-category/work/resources/work-consumption-material/work-consumption-material.component";
import {WorkMainMaterialComponent} from "./buildings/phase/work-category/work/resources/work-main-material/work-main-material.component";
import {WorkTechnicComponent} from "./buildings/phase/work-category/work/resources/work-technic/work-technic.component";
import {WorkWorkersComponent} from "./buildings/phase/work-category/work/resources/work-workers/work-workers.component";
import {WorkBrigadesComponent} from "./buildings/phase/work-category/work/resources/work-brigades/work-brigades.component";

import {BuildingService} from "./buildings/services/building.service";
import {PhaseService} from "./buildings/phase/services/phase.service";
import {WorkCategoryService} from "./buildings/phase/work-category/services/work-category.service";
import {WorkService} from "./buildings/phase/work-category/work/services/work.service";
import {HasToBeDoneAfterWorkService} from "./buildings/phase/work-category/work/services/has-to-be-done-after-work.service";
import {WorkResourcesComponent} from "./buildings/phase/work-category/work/resources/work-resources.component";
import {WorkBuildingMaterialService} from "./buildings/phase/work-category/work/resources/work-building-material/services/WorkBuildingMaterialService";
import {WorkConsumptionMaterialService} from "./buildings/phase/work-category/work/resources/work-consumption-material/services/work-consumption-material.service";
import {WorkMainMaterialService} from "./buildings/phase/work-category/work/resources/work-main-material/services/work-main-material.service";
import {WorkTechnicService} from "./buildings/phase/work-category/work/resources/work-technic/services/work-technic.service";
import {WorkWorkerService} from "./buildings/phase/work-category/work/resources/work-workers/services/work-worker.service";
import {WorkBrigadeService} from "./buildings/phase/work-category/work/resources/work-brigades/services/work-brigade.service";
import { BudgetComponent } from './budget/budget.component';
import { WorkResourcesListComponent } from './buildings/phase/work-category/work/resources/all-resources-list/work-resources-list.component';

@NgModule({
  declarations:[
    BuildingsComponent,

    PhaseComponent,

    WorkCategoryComponent,

    WorkComponent,

    WorkResourcesComponent,

    WorkBuildingMaterialComponent,

    WorkConsumptionMaterialComponent,

    WorkMainMaterialComponent,

    WorkTechnicComponent,

    WorkWorkersComponent,

    WorkBrigadesComponent,

    BudgetComponent,

    WorkResourcesListComponent
  ],
  imports: [
    SharedModule
  ],
  providers: [
    BuildingService,

    PhaseService,

    WorkCategoryService,

    WorkService,

    HasToBeDoneAfterWorkService,

    WorkBuildingMaterialService,

    WorkConsumptionMaterialService,

    WorkMainMaterialService,

    WorkTechnicService,

    WorkWorkerService,

    WorkBrigadeService
  ]
})
export class CostEstimatorModule{

}
