import { Component, OnInit } from '@angular/core';
import {PaymentTypeService} from "../services/paymentType.service";

@Component({
  selector: 'app-paymentTypes',
  templateUrl: './paymentTypes.component.html',
  styleUrls: ['./paymentTypes.component.css']
})
export class PaymentTypesComponent implements OnInit {
  cols: any[] = [
    { field : 'name', header: 'სახელი', type: 'input'}
  ]

  title = 'ანაზღაურების ტიპები';
  detailTitle = 'ანაზღაურების დეტალები';

  constructor(public service: PaymentTypeService) { }

  ngOnInit() {
  }
}
