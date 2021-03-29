import {Component, OnInit} from '@angular/core';
import {WorkerService} from "../services/worker.service";
import {WorkerCategoryService} from "../services/worker-category.service";
import {PaymentTypeService} from "../services/paymentType.service";
import {environment} from "../../../../../environments/environment";

@Component({
  selector: 'app-workers',
  templateUrl: './workers.component.html',
  styleUrls: ['./workers.component.css']
})
export class WorkersComponent implements OnInit {

  cols: any[] = [
    {
      field: 'name', header: 'დასახელება', type: 'input', denyedit: true
    },
    {
      field: 'categoryId',
      showfield: 'categoryName',
      header: 'კატეგორია',
      type: 'select',
      service: this.categoryService,
      selectfield: 'name'
    },
    {
      field: 'paymentTypeId',
      showfield: 'paymentTypeName',
      header: 'ანაზღაურების ტიპი',
      type: 'select',
      service: this.paymentTypeService,
      selectfield: 'name'
    },
    {
      field: 'salary', header: 'ანაზღაურება', type: 'number', step: 1, min: 0
    },
    {
      field: 'file', header: 'ხელშეკრულება', type: 'file', hideFromTable: true, url: `${environment.apiUrl}/file`
    },
    {
      field: 'coefficient', header: 'კოეფიციენტი', type: 'number', step: 0.1, min: 0, max: 1, default: 1
    },
    {
      field: 'comment', header: 'კომენტარი', type: 'input', optional: true
    }
  ];

  title = 'მუშახელები';
  detailTitle = 'მუშახელის დეტალები';

  constructor(public service: WorkerService, public categoryService: WorkerCategoryService, public paymentTypeService: PaymentTypeService) {
  }

  ngOnInit() {
  }
}
