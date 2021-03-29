import {Component} from '@angular/core';
import {BrigadeCategoryService} from "../../services/brigade-category.service";

@Component({
  selector: 'app-brigade-categories',
  templateUrl: './brigade-categories.component.html',
  styleUrls: ['./brigade-categories.component.css']
})
export class BrigadeCategoriesComponent {
  cols: any[] = [
    { field : 'name', header: 'სახელი', type: 'input'}
  ]

  title = 'ბრიგადის კატეგორიები';
  detailTitle = 'კატეგორიის დეტალები';

  constructor(public categoryService: BrigadeCategoryService) { }

}
