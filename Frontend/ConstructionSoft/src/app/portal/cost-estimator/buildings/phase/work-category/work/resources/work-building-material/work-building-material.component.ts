import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChange, SimpleChanges} from '@angular/core';
import {WorkBuildingMaterialService} from "./services/WorkBuildingMaterialService";
import {BuildingMaterialService} from "../../../../../../../admin/material/services/building-material.service";

@Component({
  selector: 'app-work-building-material',
  templateUrl: './work-building-material.component.html',
  styleUrls: ['./work-building-material.component.css']
})
export class WorkBuildingMaterialComponent implements OnInit, OnChanges {

  cols: any[] = [
    {
      field: 'id', header: 'მასალა', type: 'select', service: this.buildingMaterialService, hideFromTable: true,
      selectfield: 'description', showfield: 'materialName', denyedit: true
    },

    {field: 'materialName', header: 'დასახელება'},
    {field: 'count', header: 'რაოდენობა', type: 'number', step: 1, min: 0},
    {field: 'materialPrice', header: 'ღირებულება', type: 'info'},
    {field: 'materialCoefficient', header: 'კოეფიციენტი', type: 'info'},
    {field: 'materialDimension', header: 'განზომილება', type: 'info'},
    {field: 'materialCategory', header: 'კატეგორია', type: 'info'},
    {field: 'fullPrice', header: 'ჯამური ღირებულება', type: 'info'}
  ];

  @Input() workId: number;
  @Input() fullPrice: number;
  @Input() extraPricePercent: number;

  @Output() itemUpdated = new EventEmitter<void>();

  summary: string;

  constructor(public service: WorkBuildingMaterialService, public buildingMaterialService: BuildingMaterialService) {
  }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges) {
    const workId: SimpleChange = changes.workId;
    if (workId != null && workId.currentValue != null) {
      this.service.setWorkId(workId.currentValue);
    }

    if (changes.fullPrice != null || changes.extraPricePercent != null) {
      this.summary = `${this.extraPricePercent > 0
        ? ("ღირებულება: " + this.round(this.fullPrice) + ' ლარი | ფასნამატი: ' + this.round(this.fullPrice * this.extraPricePercent / 100) + ' ლარი | ')
        : ''}მთლიანი ღირებულება: ${ this.round(this.fullPrice + this.fullPrice * this.extraPricePercent / 100)} ლარი`;
    }
  }

  round(number) {
    return Math.round(number * 100) / 100;
  }
}
