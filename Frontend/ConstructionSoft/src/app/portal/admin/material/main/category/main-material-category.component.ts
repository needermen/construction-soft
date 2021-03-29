import { Component} from '@angular/core';
import {MainMaterialCategoryService} from "../../services/main-material-category.service";

@Component({
  selector: 'app-mainmaterialcategory',
  templateUrl: './main-material-category.component.html',
  styleUrls: ['./main-material-category.component.css']
})
export class MainMaterialCategoryComponent {

  cols: any[] = [
    { field : 'name', header: 'სახელი', type: 'input'}
  ]

  title = 'ძირითადი მასალის კატეგორიები';
  detailTitle = 'კატეგორიის დეტალები';

  constructor(public categoryService: MainMaterialCategoryService) { }

}
