import {Component} from '@angular/core';
import {WorkerCategoryService} from "../../services/worker-category.service";

@Component({
  selector: 'app-worker-categories',
  templateUrl: './worker-categories.component.html',
  styleUrls: ['./worker-categories.component.css']
})
export class WorkerCategoriesComponent {
  cols: any[] = [
    { field : 'name', header: 'სახელი', type: 'input'}
  ]

  title = 'მუშახელის კატეგორიები';
  detailTitle = 'კატეგორიის დეტალები';

  constructor(public categoryService: WorkerCategoryService) { }

}
