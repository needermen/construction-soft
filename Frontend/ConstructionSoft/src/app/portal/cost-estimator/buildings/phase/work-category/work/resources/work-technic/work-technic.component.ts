import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChange, SimpleChanges} from '@angular/core';
import {WorkTechnicService} from "./services/work-technic.service";
import {TechnicService} from "../../../../../../../admin/technics/services/technic.service";

@Component({
  selector: 'app-work-technic',
  templateUrl: './work-technic.component.html',
  styleUrls: ['./work-technic.component.css']
})
export class WorkTechnicComponent implements OnInit, OnChanges {

  cols: any[] = [
    {
      field: 'id', header: 'მასალა', type: 'select', service: this.technicService, hideFromTable: true,
      selectfield: 'description', showfield: 'technicName', denyedit: true
    },

    {field: 'technicName', header: 'დასახელება'},
    {field: 'count', header: 'რაოდენობა', type: 'number', step: 1, min: 0},
    {field: 'technicPrice', header: 'ღირებულება', type: 'info'},
    {field: 'technicCoefficient', header: 'კოეფიციენტი', type: 'info'},
    {field: 'technicDimension', header: 'განზომილება', type: 'info'},
    {field: 'technicCategory', header: 'კატეგორია', type: 'info'},
    {field: 'fullPrice', header: 'ჯამური ღირებულება', type: 'info'}
  ];

  @Input() workId: number;
  @Input() fullPrice: number;
  @Input() extraPricePercent: number;

  @Output() itemUpdated = new EventEmitter<void>();

  constructor(public service: WorkTechnicService, public technicService: TechnicService) {
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
