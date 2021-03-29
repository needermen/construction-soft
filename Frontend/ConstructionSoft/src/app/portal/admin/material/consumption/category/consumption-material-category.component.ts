import { Component, OnInit } from '@angular/core';
import {ConsumptionMaterialCategoryService} from "../../services/consumption-material-category.service";

@Component({
  selector: 'app-consumptionmaterialcategory',
  templateUrl: './consumption-material-category.component.html',
  styleUrls: ['./consumption-material-category.component.css']
})
export class ConsumptionMaterialCategoryComponent {

  cols: any[] = [
    { field : 'name', header: 'სახელი', type: 'input'}
  ]

  title = 'სახარჯი მასალის კატეგორიები';
  detailTitle = 'კატეგორიის დეტალები';

  constructor(public categoryService: ConsumptionMaterialCategoryService) { }

}
