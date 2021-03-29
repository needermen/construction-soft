import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {ResourceBase} from "../../models/resourceBase";
import {WorkWorkerService} from "../work-workers/services/work-worker.service";
import {WorkBrigadeService} from "../work-brigades/services/work-brigade.service";
import {WorkTechnicService} from "../work-technic/services/work-technic.service";
import {WorkMainMaterialService} from "../work-main-material/services/work-main-material.service";
import {WorkBuildingMaterialService} from "../work-building-material/services/WorkBuildingMaterialService";
import {WorkConsumptionMaterialService} from "../work-consumption-material/services/work-consumption-material.service";
import {ActivatedRoute} from "@angular/router";
import {forkJoin, Observable} from "rxjs";

@Component({
  selector: 'app-work-resources-list',
  templateUrl: './work-resources-list.component.html',
  styleUrls: ['./work-resources-list.component.css']
})
export class WorkResourcesListComponent implements OnChanges {
  resources: ResourceBase[];
  resourcesOnPage: ResourceBase[];
  loading: boolean = false;

  @Input() id: number;

  rows: number = 5;

  filterText: string;

  constructor(private route: ActivatedRoute,
              private workWorkerService: WorkWorkerService,
              private workBrigadeService: WorkBrigadeService, private workTechnicService: WorkTechnicService,
              private workMainMaterialService: WorkMainMaterialService, private workBuildingMaterialService: WorkBuildingMaterialService,
              private workConsumptionMaterialService: WorkConsumptionMaterialService) {
  }


  ngOnChanges(changes: SimpleChanges): void {
    if (changes.id != null && changes.id.currentValue > 0) {
      this.collectResources(changes.id.currentValue, '');
    }
  }

  filterData(filter) {
    this.collectResources(this.id, filter);
  }

  collectResources(id, filter) {
    this.resources = [];
    this.loading = true;
    this.filterText = filter;

    this.workWorkerService.setWorkId(id);
    this.workBrigadeService.setWorkId(id);
    this.workTechnicService.setWorkId(id);
    this.workBuildingMaterialService.setWorkId(id);
    this.workConsumptionMaterialService.setWorkId(id);
    this.workMainMaterialService.setWorkId(id);

    forkJoin([this.workWorkerService.get(0, 1000000, filter),
      this.workBrigadeService.get(0, 1000000, filter),
      this.workTechnicService.get(0, 1000000, filter),
      this.workBuildingMaterialService.get(0, 1000000, filter),
      this.workConsumptionMaterialService.get(0, 1000000, filter),
      this.workMainMaterialService.get(0, 1000000, filter)
    ]).subscribe(result => {
      this.addWorkers(result[0]);
      this.addBrigades(result[1]);
      this.addTechnics(result[2]);
      this.addBuildingMaterials(result[3]);
      this.addConsumptionMaterials(result[4]);
      this.addMainMaterials(result[5]);

      this.onPageChange({first: 0});

      this.loading = false;
    });
  }

  addWorkers(workers) {
    workers.items.forEach(worker => {
      var resource = <ResourceBase> {
        name: worker.workerName,
        category: worker.workerCategory,
        coefficient: worker.workerCoefficient,
        count: worker.count,
        fullPrice: worker.fullPrice,
        salary: worker.workerSalary,
        paymentType: worker.workerPaymentType
      };

      this.resources.push(resource);
    });
  }

  addBrigades(brigades) {
    brigades.items.forEach(brigade => {
      var resource = <ResourceBase>{
        name: brigade.brigadeName,
        category: brigade.brigadeCategory,
        fullPrice: brigade.fullPrice,
        count: brigade.count,
        salary: brigade.brigadeSalary,
        paymentType: brigade.brigadePaymentType
      };

      this.resources.push(resource);
    })
  }

  addTechnics(technics) {
    technics.items.forEach(technic => {
      var resource = <ResourceBase>{
        name: technic.technicName,
        category: technic.technicCategory,
        dimension: technic.technicDimension,
        coefficient: technic.technicCoefficient,
        perPrice: technic.technicPrice,
        count: technic.count,
        fullPrice: technic.fullPrice,
      };

      this.resources.push(resource);
    })
  }

  addBuildingMaterials(materials) {
    materials.items.forEach(material => {
      var resource = <ResourceBase> {
        name: material.materialName,
        category: material.materialCategory,
        dimension: material.materialDimension,
        coefficient: material.materialCoefficient,
        fullPrice: material.fullPrice,
        count: material.count,
        perPrice: material.materialPrice
      };

      this.resources.push(resource);
    })
  }

  addConsumptionMaterials(materials) {
    materials.items.forEach(material => {
      var resource = <ResourceBase> {
        name: material.materialName,
        category: material.materialCategory,
        dimension: material.materialDimension,
        coefficient: material.materialCoefficient,
        fullPrice: material.fullPrice,
        count: material.count,
        perPrice: material.materialPrice
      };

      this.resources.push(resource);
    })
  }

  addMainMaterials(materials) {
    materials.items.forEach(material => {
      var resource = <ResourceBase> {
        name: material.materialName,
        category: material.materialCategory,
        dimension: material.materialDimension,
        coefficient: material.materialCoefficient,
        depreciation: material.materialDepreciation,
        fullPrice: material.fullPrice,
        count: material.count,
        perPrice: material.materialPrice
      };

      this.resources.push(resource);
    })
  }

  onPageChange(event) {
    this.resourcesOnPage = this.resources.slice(event.first, event.first + this.rows);
  }
}
