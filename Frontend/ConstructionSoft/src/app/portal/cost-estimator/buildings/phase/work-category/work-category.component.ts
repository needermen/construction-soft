import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {CrudComponent} from "../../../../../shared/components/crud/crud.component";
import {WorkCategory} from "./models/work-category";
import {WorkCategoryService} from "./services/work-category.service";

@Component({
  selector: 'app-work-category',
  templateUrl: './work-category.component.html',
  styleUrls: ['./work-category.component.css']
})
export class WorkCategoryComponent implements OnInit {

  //crud
  @ViewChild(CrudComponent)
  private crudComponent: CrudComponent<WorkCategory>;

  cols: any[] = [
    { field : 'name', header: 'დასახელება', type: 'input' },
  ];

  detailTitle = 'სამუშაო ჯგუფის დეტალები';


  //parent
  @Output() workCategoryUpdated = new EventEmitter<void>();

  constructor(public service: WorkCategoryService) { }

  ngOnInit() {
  }

  //methods

  displayNewDialog(workCategory){
    this.crudComponent.showNewItemForm(workCategory);
  }

  displayEditDialog(id){
    this.crudComponent.showEditForm(id);
  }

}
