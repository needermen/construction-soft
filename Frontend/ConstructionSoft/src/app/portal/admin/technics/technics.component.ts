import {Component, OnInit} from '@angular/core';
import {TechnicService} from "./services/technic.service";
import {TechnicCategoryService} from "./services/technic-category.service";
import {TechnicDimensionService} from "./services/technic-dimension.service";

@Component({
  selector: 'app-technics',
  templateUrl: './technics.component.html',
  styleUrls: ['./technics.component.css']
})
export class TechnicsComponent implements OnInit {
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

  title = 'ტექნიკა';
  detailTitle = 'ტექნიკის დეტალები';

  constructor(public technicService: TechnicService, public categoryService: TechnicCategoryService, public dimensionService: TechnicDimensionService) {
  }

  ngOnInit() {
  }
}
