import { Component} from '@angular/core';
import {BuildingMaterialCategoryService} from "../../services/building-material-category.service";

@Component({
  selector: 'app-buildingmaterialcategory',
  templateUrl: './building-material-category.component.html',
  styleUrls: ['./building-material-category.component.css']
})
export class BuildingMaterialCategoryComponent{

  cols: any[] = [
    { field : 'name', header: 'სახელი', type: 'input'}
  ]

  title = 'სამშენებლო მასალის კატეგორიები';
  detailTitle = 'კატეგორიის დეტალები';

  constructor(public categoryService: BuildingMaterialCategoryService) { }


}
