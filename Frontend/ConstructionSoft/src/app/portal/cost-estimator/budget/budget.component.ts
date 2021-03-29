import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {TreeNode} from "primeng/api";
import {BuildingService} from "../buildings/services/building.service";
import {PhaseService} from "../buildings/phase/services/phase.service";
import {WorkCategoryService} from "../buildings/phase/work-category/services/work-category.service";
import {WorkService} from "../buildings/phase/work-category/work/services/work.service";
import {BaseBudgetModel} from "./baseBudget.model";
import {WorkWorkerService} from "../buildings/phase/work-category/work/resources/work-workers/services/work-worker.service";
import {WorkBrigadeService} from "../buildings/phase/work-category/work/resources/work-brigades/services/work-brigade.service";
import {WorkTechnicService} from "../buildings/phase/work-category/work/resources/work-technic/services/work-technic.service";
import {WorkMainMaterialService} from "../buildings/phase/work-category/work/resources/work-main-material/services/work-main-material.service";
import {WorkBuildingMaterialService} from "../buildings/phase/work-category/work/resources/work-building-material/services/WorkBuildingMaterialService";
import {WorkConsumptionMaterialService} from "../buildings/phase/work-category/work/resources/work-consumption-material/services/work-consumption-material.service";

@Component({
  selector: 'app-budget',
  templateUrl: './budget.component.html',
  styleUrls: ['./budget.component.css']
})
export class BudgetComponent implements OnInit {
  data: TreeNode[];

  constructor(private route: ActivatedRoute, private buildingService: BuildingService,
              private phaseService: PhaseService, private categoryService: WorkCategoryService,
              private workService: WorkService, private workWorkerService: WorkWorkerService,
              private workBrigadeService: WorkBrigadeService, private workTechnicService: WorkTechnicService,
              private workMainMaterialService: WorkMainMaterialService, private workBuildingMaterialService: WorkBuildingMaterialService,
              private workConsumptionMaterialService: WorkConsumptionMaterialService) {
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.collectData(params.id);
    });
  }

  collectData(id) {
    this.buildingService.GetBaseBuildingDetails(id, 'building').subscribe(building => {
      var buildingNode = <TreeNode>{
        data: <BaseBudgetModel> {
          name: building.name,
          startDate: building.startDate,
          endDate: building.endDate,
          durationInDays: building.durationInDays,
          fullPrice: building.fullPrice,
          extraPrice: building.extraPrice
        },
        children: []
      };

      this.data = [buildingNode];

      this.phaseService.GetByBuilding(id).subscribe(phases => {
        phases.items.forEach(phase => {
          var phaseNode = <TreeNode>{
            data: <BaseBudgetModel> {
              name: phase.name,
              startDate: phase.startDate,
              endDate: phase.endDate,
              durationInDays: phase.durationInDays,
              fullPrice: phase.fullPrice,
              extraPrice: phase.extraPrice
            },
            children: [],
            parent: buildingNode,
            expanded: true
          };

          this.data[0].children.push(phaseNode);

          this.categoryService.GetByPhase(phase.id).subscribe(categories => {
            categories.items.forEach(category => {
              var categoryNode = <TreeNode>{
                data: <BaseBudgetModel> {
                  name: category.name,
                  startDate: category.startDate,
                  endDate: category.endDate,
                  durationInDays: category.durationInDays,
                  fullPrice: category.fullPrice,
                  extraPrice: category.extraPrice
                },
                children: [],
                parent: phaseNode,
                expanded: true
              };

              phaseNode.children.push(categoryNode);

              this.workService.GetByWorkCategory(category.id).subscribe(works => {
                works.items.forEach(work => {
                  var workNode = <TreeNode>{
                    data: <BaseBudgetModel> {
                      name: work.name,
                      startDate: work.startDate,
                      endDate: work.endDate,
                      durationInDays: work.durationInDays,
                      fullPrice: work.fullPrice,
                      extraPrice: work.extraPrice
                    },
                    children: [],
                    parent: categoryNode,
                    expanded: true
                  };

                  this.collectResources(work, workNode);

                  categoryNode.children.push(workNode);
                });
              })
            })
          });
        })
      });
    });
  }

  collectResources(work, workNode) {
    this.workWorkerService.setWorkId(work.id);
    this.workWorkerService.get(0, 1000000, '').subscribe(workers => {
      workers.items.forEach(worker => {
        var workerNode = <TreeNode>{
          data: <BaseBudgetModel> {
            name: worker.workerName,
            fullPrice: worker.fullPrice,
            extraPrice: work.extraPricePercent * worker.fullPrice / 100,
            resourceCount: worker.count,
            resourceCategory: worker.workerCategory,
            resourcePrice: worker.workerSalary
          },
          parent: workerNode
        };

        workNode.children.push(workerNode);
      })
    });

    this.workBrigadeService.setWorkId(work.id);
    this.workBrigadeService.get(0, 1000000, '').subscribe(brigades => {
      brigades.items.forEach(brigade => {
        var brigadeNode = <TreeNode>{
          data: <BaseBudgetModel> {
            name: brigade.brigadeName,
            fullPrice: brigade.fullPrice,
            extraPrice: work.extraPricePercent * brigade.fullPrice / 100,
            resourceCount: brigade.count,
            resourceCategory: brigade.brigadeCategory,
            resourcePrice: brigade.brigadeSalary
          },
          parent: workNode
        };

        workNode.children.push(brigadeNode);
      })
    });

    this.workTechnicService.setWorkId(work.id);
    this.workTechnicService.get(0, 1000000, '').subscribe(technics => {
      technics.items.forEach(technic => {
        var technicNode = <TreeNode>{
          data: <BaseBudgetModel> {
            name: technic.technicName,
            fullPrice: technic.fullPrice,
            extraPrice: work.extraPricePercent * technic.fullPrice / 100,
            resourceCount: technic.count,
            resourceDimension: technic.technicDimension,
            resourceCategory: technic.technicCategory,
          },
          parent: workNode
        };

        workNode.children.push(technicNode);
      })
    });

    this.workBuildingMaterialService.setWorkId(work.id);
    this.workBuildingMaterialService.get(0, 1000000, '').subscribe(materials => {
      materials.items.forEach(buildingMaterial => {
        var buildingMaterialNode = <TreeNode>{
          data: <BaseBudgetModel> {
            name: buildingMaterial.materialName,
            fullPrice: buildingMaterial.fullPrice,
            extraPrice: work.extraPricePercent * buildingMaterial.fullPrice / 100,
            resourceCount: buildingMaterial.count,
            resourceDimension: buildingMaterial.materialDimension,
            resourceCategory: buildingMaterial.materialCategory,
            resourcePrice: buildingMaterial.materialPrice
          },
          parent: workNode
        };

        workNode.children.push(buildingMaterialNode);
      })
    });

    this.workConsumptionMaterialService.setWorkId(work.id);
    this.workConsumptionMaterialService.get(0, 1000000, '').subscribe(materials => {
      materials.items.forEach(consumptionMaterial => {
        var buildingMaterialNode = <TreeNode>{
          data: <BaseBudgetModel> {
            name: consumptionMaterial.materialName,
            fullPrice: consumptionMaterial.fullPrice,
            extraPrice: work.extraPricePercent * consumptionMaterial.fullPrice / 100,
            resourceCount: consumptionMaterial.count,
            resourceDimension: consumptionMaterial.materialDimension,
            resourceCategory: consumptionMaterial.materialCategory,
            resourcePrice: consumptionMaterial.materialPrice
          },
          parent: workNode
        };

        workNode.children.push(buildingMaterialNode);
      })
    });

    this.workMainMaterialService.setWorkId(work.id);
    this.workMainMaterialService.get(0, 1000000, '').subscribe(materials => {
      materials.items.forEach(mainMaterial => {
        var buildingMaterialNode = <TreeNode>{
          data: <BaseBudgetModel> {
            name: mainMaterial.materialName,
            fullPrice: mainMaterial.fullPrice,
            extraPrice: work.extraPricePercent * mainMaterial.fullPrice / 100,
            resourceCount: mainMaterial.count,
            resourceDimension: mainMaterial.materialDimension,
            resourceCategory: mainMaterial.materialCategory,
            resourcePrice: mainMaterial.materialPrice
          },
          parent: workNode
        };

        workNode.children.push(buildingMaterialNode);
      })
    });
  }
}
