import {Component} from '@angular/core';
import {TechnicCategoryService} from "../services/technic-category.service";

@Component({
  selector: 'app-techniccategories',
  templateUrl: './technic-categories.component.html',
  styleUrls: ['./technic-categories.component.css']
})
export class TechnicCategoriesComponent {
  cols: any[] = [
    { field : 'name', header: 'სახელი', type: 'input'}
  ]

  title = 'ტექნიკის კატეგორიები';
  detailTitle = 'კატეგორიის დეტალები';

  constructor(public categoryService: TechnicCategoryService) { }

}
