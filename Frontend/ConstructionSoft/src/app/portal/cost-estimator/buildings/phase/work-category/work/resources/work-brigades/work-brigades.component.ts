import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChange, SimpleChanges} from '@angular/core';
import {BrigadeService} from "../../../../../../../admin/hr/services/brigade.service";
import {WorkBrigadeService} from "./services/work-brigade.service";

@Component({
  selector: 'app-work-brigades',
  templateUrl: './work-brigades.component.html',
  styleUrls: ['./work-brigades.component.css']
})
export class WorkBrigadesComponent implements OnInit, OnChanges {

  cols: any[] = [
    {
      field: 'id', header: 'მასალა', type: 'select', service: this.brigadeService, hideFromTable: true,
      selectfield: 'name', showfield: 'brigadeName', denyedit: true
    },

    {field: 'brigadeName', header: 'დასახელება'},
    {field: 'count', header: 'რაოდენობა', type: 'number', step: 1, min: 0},
    {field: 'brigadeSalary', header: 'ხელფასი', type: 'info'},
    {field: 'brigadePaymentType', header: 'ანაზღაურება', type: 'info'},
    {field: 'brigadeCategory', header: 'კატეგორია', type: 'info'},
    {field: 'fullPrice', header: 'ჯამური ღირებულება', type: 'info'}
  ];

  @Input() workId: number;
  @Input() fullPrice: number;
  @Input() extraPricePercent: number;

  @Output() itemUpdated = new EventEmitter<void>();


  constructor(public service: WorkBrigadeService, public brigadeService: BrigadeService) {
  }

  ngOnInit() {
  }

  summary: string;

  ngOnChanges(changes: SimpleChanges) {
    const workId: SimpleChange = changes.workId;
    if (workId != null && workId.currentValue != null) {
      this.service.setWorkId(workId.currentValue);
    }

    if (changes.fullPrice != null || changes.extraPricePercent != null) {
      this.summary = `${this.extraPricePercent > 0
        ? ("ღირებულება: " + this.round(this.fullPrice) + ' ლარი | ფასნამატი: ' + this.round(this.fullPrice * this.extraPricePercent / 100) + ' ლარი | ')
        : ''}მთლიანი ღირებულება: ${ this.round(this.fullPrice + this.fullPrice * this.extraPricePercent / 100)} ლარი`;
    }
  }

  round(number) {
    return Math.round(number * 100) / 100;
  }
}
