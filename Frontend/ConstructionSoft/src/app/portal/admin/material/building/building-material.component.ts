import {Component, OnInit} from '@angular/core';
import {MaterialDimensionService} from "../services/material-dimension.service";
import {BuildingMaterialCategoryService} from "../services/building-material-category.service";
import {BuildingMaterialService} from "../services/building-material.service";

@Component({
  selector: 'app-buildingmaterial',
  templateUrl: './building-material.component.html',
  styleUrls: ['./building-material.component.css']
})
export class BuildingMaterialComponent implements OnInit {

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

  title = 'სამშენებლო მასალები';
  detailTitle = 'მასალის დეტალები';

  constructor(public service: BuildingMaterialService, public categoryService: BuildingMaterialCategoryService, public dimensionService: MaterialDimensionService) {
  }

  ngOnInit() {
  }

}
