import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {Work} from "./models/work";
import {WorkService} from "./services/work.service";
import {CrudComponent} from "../../../../../../shared/components/crud/crud.component";
import {HasToBeDoneAfterWorkService} from "./services/has-to-be-done-after-work.service";

@Component({
  selector: 'app-work',
  templateUrl: './work.component.html',
  styleUrls: ['./work.component.css']
})
export class WorkComponent implements OnInit {

  //crud
  @ViewChild(CrudComponent)
  private crudComponent: CrudComponent<Work>;

  cols: any[] = [
    {field: 'name', header: 'დასახელება', type: 'input'},
    {field: 'startDate', header: 'დაწყების თარიღი', type: 'date'},
    {field: 'durationInDays', header: 'სამუშაოს ხანგძლივობა (დღე)', type: 'number', step: '1', min: 0},
    {field: 'endDate', header: 'დასრულების თარიღი', type: 'date'},
    {
      field: 'hasToBeDoneAfterId',
      header: 'დამოკიდებულია სამუშაოზე',
      type: 'dropdown',
      service: this.hasToBeDoneAfterWorkService,
      fillService: this.service,
      selectfield: 'name',
      optional: true
    },
  ];

  detailTitle = 'სამუშაოს დეტალები';


  //parent
  @Output() workUpdated = new EventEmitter<void>();

  constructor(public service: WorkService, public hasToBeDoneAfterWorkService: HasToBeDoneAfterWorkService) {

  }

  ngOnInit() {
  }

  //methods

  showNewDialog(work) {
    this.hasToBeDoneAfterWorkService.setWorkCategoryId(work.workCategoryId);
    this.hasToBeDoneAfterWorkService.setWorkId(work.id);

    var col = this.crudComponent.getColByFieldName('hasToBeDoneAfterId');
    this.crudComponent.loadDropdown(col);

    this.crudComponent.showNewItemForm(work);
  }

  showEditDialog(id, workCategoryId) {
    this.hasToBeDoneAfterWorkService.setWorkCategoryId(workCategoryId);
    this.hasToBeDoneAfterWorkService.setWorkId(id);

    var col = this.crudComponent.getColByFieldName('hasToBeDoneAfterId');
    this.crudComponent.loadDropdown(col);

    this.crudComponent.showEditForm(id);
  }

  onUserInput(event) {
    if (event.col != null) {
      var currentWork = this.crudComponent.item;

      switch (event.col.field) {
        case 'hasToBeDoneAfterId':
          var selectedWork = this.crudComponent.selectSelected['hasToBeDoneAfterId'];
          if (selectedWork != null && selectedWork.id > 0) {
            this.service.getById(selectedWork.id).subscribe((result) => {
              if (result != null) {
                currentWork.startDate = new Date(result.endDate);
                var startDateCol = this.crudComponent.getColByFieldName('startDate');
                this.onUserInput({col: startDateCol});
              }
            });
          }
          break;
        case 'endDate':
          if (currentWork.startDate != null && currentWork.endDate != null) {
            currentWork.durationInDays = (currentWork.endDate.getTime() - currentWork.startDate.getTime()) / (1000 * 60 * 60 * 24);
          }
          break;
        case 'startDate':
          if (currentWork.durationInDays != null && currentWork.startDate != null) {
            currentWork.endDate = new Date();
            currentWork.endDate.setTime(currentWork.startDate.getTime() + currentWork.durationInDays * (1000 * 60 * 60 * 24));
          }
          break;
        case 'durationInDays':
          if (currentWork.durationInDays != null && currentWork.startDate != null) {
            currentWork.endDate = new Date();
            currentWork.endDate.setTime(currentWork.startDate.getTime() + currentWork.durationInDays * (1000 * 60 * 60 * 24));
          }
          break;
      }
    }
  }
}
