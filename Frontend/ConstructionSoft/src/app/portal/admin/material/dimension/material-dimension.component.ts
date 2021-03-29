import { Component, OnInit } from '@angular/core';
import {MaterialDimensionService} from "../services/material-dimension.service";

@Component({
  selector: 'app-materialdimension',
  templateUrl: './material-dimension.component.html',
  styleUrls: ['./material-dimension.component.css']
})
export class MaterialDimensionComponent implements OnInit {

  cols: any[] = [
    { field : 'name', header: 'სახელი', type: 'input'}
  ]

  title = 'მასალის განზომილებები';
  detailTitle = 'განზომილების დეტალები';

  constructor(public dimensionService: MaterialDimensionService) { }

  ngOnInit() {
  }
}
