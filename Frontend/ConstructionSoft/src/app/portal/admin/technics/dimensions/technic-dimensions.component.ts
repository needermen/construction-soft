import { Component, OnInit } from '@angular/core';
import {TechnicCategoryService} from "../services/technic-category.service";
import {TechnicDimensionService} from "../services/technic-dimension.service";

@Component({
  selector: 'app-technicdimensions',
  templateUrl: './technic-dimensions.component.html',
  styleUrls: ['./technic-dimensions.component.css']
})
export class TechnicDimensionsComponent implements OnInit {
  cols: any[] = [
    { field : 'name', header: 'სახელი', type: 'input'}
  ]

  title = 'ტექნიკის განზომილებები';
  detailTitle = 'განზომილების დეტალები';

  constructor(public dimensionService: TechnicDimensionService) { }

  ngOnInit() {
  }
}
