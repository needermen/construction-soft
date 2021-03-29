import {Component, OnInit} from '@angular/core';
import {PaymentTypeService} from "../services/paymentType.service";
import {BrigadeCategoryService} from "../services/brigade-category.service";
import {BrigadeService} from "../services/brigade.service";
import {environment} from "../../../../../environments/environment";

@Component({
  selector: 'app-brigades',
  templateUrl: './brigades.component.html',
  styleUrls: ['./brigades.component.css']
})
export class BrigadesComponent implements OnInit {

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
    }
  ];

  title = 'ბრიგადები';
  detailTitle = 'ბრიგადის დეტალები';

  constructor(public service: BrigadeService, public categoryService: BrigadeCategoryService, public paymentTypeService: PaymentTypeService) {
  }

  ngOnInit() {
  }
}
