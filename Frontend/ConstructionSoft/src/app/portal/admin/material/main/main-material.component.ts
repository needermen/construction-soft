import {Component, OnInit} from '@angular/core';
import {MaterialDimensionService} from "../services/material-dimension.service";
import {MainMaterialService} from "../services/main-material.service";
import {MainMaterialCategoryService} from "../services/main-material-category.service";

@Component({
  selector: 'app-mainmaterial',
  templateUrl: './main-material.component.html',
  styleUrls: ['./main-material.component.css']
})
export class MainMaterialComponent implements OnInit {

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
      field: 'depreciation', header: 'ცვეთა (%)', type: 'number', step: '0.1'
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

  title = 'ძირითადი მასალები';
  detailTitle = 'მასალის დეტალები';

  constructor(public service: MainMaterialService, public categoryService: MainMaterialCategoryService, public dimensionService: MaterialDimensionService) {
  }

  ngOnInit() {
  }

}
