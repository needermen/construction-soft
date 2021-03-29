import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChange, SimpleChanges} from '@angular/core';
import {WorkWorkerService} from "./services/work-worker.service";
import {WorkerService} from "../../../../../../../admin/hr/services/worker.service";

@Component({
  selector: 'app-work-workers',
  templateUrl: './work-workers.component.html',
  styleUrls: ['./work-workers.component.css']
})
export class WorkWorkersComponent implements OnInit, OnChanges {

  cols: any[] = [
    {
      field: 'id', header: 'მასალა', type: 'select', service: this.workerService, hideFromTable: true,
      selectfield: 'name', showfield: 'workerName', denyedit: true
    },

    {field: 'workerName', header: 'დასახელება'},
    {field: 'count', header: 'რაოდენობა', type: 'number', step: 1, min: 0},
    {field: 'workerSalary', header: 'ხელფასი', type: 'info'},
    {field: 'workerCoefficient', header: 'კოეფიციენტი', type: 'info'},
    {field: 'workerPaymentType', header: 'ანაზღაურება', type: 'info'},
    {field: 'workerCategory', header: 'კატეგორია', type: 'info'},
    {field: 'fullPrice', header: 'ჯამური ღირებულება', type: 'info'}
  ];

  @Input() workId: number;
  @Input() fullPrice: number;
  @Input() extraPricePercent: number;

  @Output() itemUpdated = new EventEmitter<void>();

  constructor(public service: WorkWorkerService, public workerService: WorkerService) {
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
