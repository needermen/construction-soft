import {Component, OnInit} from '@angular/core';
import {MaterialDimensionService} from "../services/material-dimension.service";
import {ConsumptionMaterialService} from "../services/consumption-material.service";
import {ConsumptionMaterialCategoryService} from "../services/consumption-material-category.service";

@Component({
  selector: 'app-consumptionmaterial',
  templateUrl: './consumption-material.component.html',
  styleUrls: ['./consumption-material.component.css']
})
export class ConsumptionMaterialComponent implements OnInit {

  cols: any[] = [
    {field: 'name', header: 'დასახელება', type: 'input', denyedit: true},
    {
      field: 'categoryId',
      showfield: 'category.name',
      header: 'კატეგორია',
      type: 'select',
      service: this.categoryService,
      selectfield: 'name'
    },
    {
      field: 'dimensionId',
      showfield: 'dimension.name',
      header: 'განზომილება',
      type: 'select',
      service: this.dimensionService,
      selectfield: 'name'
    },
    {
      field: 'price', header: 'ერთეულის ღირებულება (ლარი)', type: 'number', step: 0.1, min: 0
    },
    {
      field: 'coefficient', header: 'კოეფიციენტი', type: 'number', step: 0.1, min: 0, max: 1, default: 1
    },
    {
      field: 'comment', header: 'კომენტარი', type: 'input', optional: true
    }
  ];

  title = 'სახარჯი მასალები';
  detailTitle = 'მასალის დეტალები';

  constructor(public service: ConsumptionMaterialService, public categoryService: ConsumptionMaterialCategoryService, public dimensionService: MaterialDimensionService) {
  }

  ngOnInit() {
  }

}
