import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import { PhaseService } from "./services/phase.service";
import {CrudComponent} from "../../../../shared/components/crud/crud.component";
import {Phase} from "./models/phase";

@Component({
  selector: 'app-phase',
  templateUrl: './phase.component.html',
  styleUrls: ['./phase.component.css']
})
export class PhaseComponent implements OnInit {

  //crud

  @ViewChild(CrudComponent)
  private crudComponent: CrudComponent<Phase>;

  cols: any[] = [
    { field : 'name', header: 'დასახელება', type: 'input' },
  ];

  detailTitle = 'ფაზის დეტალები';


  //parent

  @Output() phaseUpdated = new EventEmitter<void>();

  constructor(public service: PhaseService) { }

  ngOnInit() {
  }


  //methods

  displayNewDialog(phase){
    this.crudComponent.showNewItemForm(phase);
  }

  displayEditDialog(id){
    this.crudComponent.showEditForm(id);
  }

}
