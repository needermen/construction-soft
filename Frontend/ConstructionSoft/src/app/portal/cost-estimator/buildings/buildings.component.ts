import {Component, OnInit, ViewChild} from '@angular/core';
import {BuildingService} from "./services/building.service";
import {Building} from "./models/building";
import {CrudComponent} from "../../../shared/components/crud/crud.component";
import {MessageService, TreeNode} from 'primeng/api';
import {PhaseComponent} from "./phase/phase.component";
import {PhaseService} from "./phase/services/phase.service";
import {WorkCategoryComponent} from "./phase/work-category/work-category.component";
import {WorkCategoryService} from "./phase/work-category/services/work-category.service";
import {WorkComponent} from "./phase/work-category/work/work.component";
import {WorkService} from "./phase/work-category/work/services/work.service";
import {BuildingBase} from "./models/buildingBase";
import {WorkResourcesComponent} from "./phase/work-category/work/resources/work-resources.component";

@Component({
  selector: 'app-buildings',
  templateUrl: './buildings.component.html',
  styleUrls: ['./buildings.component.css']
})
export class BuildingsComponent implements OnInit {

  //// child components

  //crud
  @ViewChild(CrudComponent)
  private crudComponent: CrudComponent<Building>;

  cols: any[] = [
    { field : 'name', header: 'დასახელება', type: 'input', denyedit: true },
    { field : 'statusName', header: 'სტატუსი', type: 'info' },
    { field : 'ხარჯთაღრიცხვა', header: '', type: 'link', link: '/portal/cost-estimator/budget'},
    { field : 'taxCode', header: 'საიდენტიფიკაციო კოდი', type: 'input',hideFromTable: true, optional: true },
    { field : 'phoneNumber', header: 'ტელეფონი', type: 'input', hideFromTable: true, optional: true },
    { field : 'legalAddress', header: 'იურიდიული მისამართი', type: 'input', hideFromTable: true, optional: true },
    { field : 'actualAddress', header: 'ფაქტიური მისამართი', type: 'input', hideFromTable: true, optional: true },
    { field : 'bank', header: 'ბანკი', type: 'input', hideFromTable: true, optional: true },
    { field : 'bankCode', header: 'ბანკის კოდი', type: 'input', hideFromTable: true, optional: true },
    { field : 'accountNumber', header: 'ანგარიშის ნომერი', type: 'input', hideFromTable: true, optional: true },
    { field : 'agreementNumber', header: 'ხელშეკრულების ნომერი', type: 'input', hideFromTable: true, optional: true },
    { field : 'agreementDate', header: 'ხელშეკრულების დადების თარიღი', type: 'date', hideFromTable: true, optional: true },
    { field : 'email', header: 'E-mail', type: 'input', hideFromTable: true, optional: true },
    { field : 'director', header: 'დირექტორი', type: 'input', hideFromTable: true, optional: true },
    { field : 'directorPhoneNumber', header: 'დირექტორის ტელეფონის ნომერი', type: 'input', hideFromTable: true, optional: true },
  ];
  title = 'ობიექტები';
  detailTitle = 'ობიექტის დეტალები';

  //phase
  @ViewChild(PhaseComponent)
  private phaseComponent: PhaseComponent;

  @ViewChild(WorkCategoryComponent)
  private workCategoryComponent: WorkCategoryComponent;

  @ViewChild(WorkComponent)
  private workComponent: WorkComponent;

  @ViewChild(WorkResourcesComponent)
  private workResoucesComponent: WorkResourcesComponent;

  //// end of child components

  building: Building;

  // building tree
  treeNodes: TreeNode[];
  selectedNode: TreeNode;
  showTree: boolean = false;

  // schedule
  events: any;
  header: any = {
    right: 'prev,next today',
    center: '',
    left: 'title'
  };
  schedule: any;

  // details
  details: BuildingBase;

  constructor(public service: BuildingService, private phaseService: PhaseService,
              private messageService: MessageService, private workCategoryService: WorkCategoryService,
              private workService: WorkService) { }

  //methods

  initTree(){
    this.treeNodes = [
      {
        type: 'building',
        data: { id: this.building.id },
        expanded: true,
        children: []
      }
    ];
  }

  reloadPhases(){
    this.selectedNode = null;

    this.phaseService.GetByBuilding(this.building.id).subscribe((result)=>{
      this.treeNodes[0].children = [];

      for (let phase of result.items.sort((a, b) => a.order > b.order ? 1 : 0)){
        let phaseNode = {
          label : phase.name,
          type : 'phase',
          expanded: true,
          data : { id : phase.id, order: phase.order },
          children: []
        };

        var index = this.treeNodes[0].children.push(phaseNode) - 1;
        this.reloadWorkCategories(this.treeNodes[0].children[index]);
      }

      this.addNewPhaseNode();
    });
  }

  addNewPhaseNode(){
    this.treeNodes[0].children.push({
      type: 'addPhase',
    });
  }

  reloadWorkCategories(phaseNode){
    this.workCategoryService.GetByPhase(phaseNode.data.id).subscribe((result)=>{
      phaseNode.children = [];

      if(result.total > 0) {
        for (let workCategory of result.items.sort((a, b) => a.order > b.order ? 1 : 0)) {
          let workCategoryNode = {
            label: workCategory.name,
            type: 'workCategory',
            expanded: true,
            data: {id: workCategory.id, phaseId: phaseNode.data.id, order: workCategory.order}
          };

          var index =  phaseNode.children.push(workCategoryNode) - 1;
          this.reloadWorks(phaseNode.children[index]);
        }
      }

      this.addNewWorkCategoryNode(phaseNode);
    });
  }

  addNewWorkCategoryNode(phaseNode){
    phaseNode.children.push({
      type: 'addWorkCategory',
      data: { phaseId: phaseNode.data.id }
    });
  }

  reloadWorks(workCategoryNode){
    this.workService.GetByWorkCategory(workCategoryNode.data.id).subscribe((result)=>{
      workCategoryNode.children = [];

      if(result.total > 0) {
        for (let work of result.items.sort((a, b) => a.order > b.order ? 1 : 0)) {
          let workNode = {
            label: work.name,
            type: 'work',
            expanded: true,
            data: {id: work.id, workCategoryId: workCategoryNode.data.id, order: work.order}
          };

          workCategoryNode.children.push(workNode);
        }
      }

      this.addNewWorkNode(workCategoryNode)
    });
  }

  addNewWorkNode(workCategoryNode){
    workCategoryNode.children.push({
      type: 'addWork',
      data: { workCategoryId: workCategoryNode.data.id }
    });
  }


  //events

  ngOnScheduleInit(schedule){
    this.schedule = schedule;
  }

  ngOnInit(){
    this.events = [];
  }

  onNodeSelect(event) {
    let node = event.node;

    switch (node.type) {
      case 'addPhase':
        this.phaseComponent.displayNewDialog( { buildingId : this.building.id });
        break;
      case 'addWorkCategory':
        this.workCategoryComponent.displayNewDialog( { phaseId : node.data.phaseId });
        break;
      case 'addWork':
        this.workComponent.showNewDialog( { workCategoryId : node.data.workCategoryId });
        break;
      case 'work':
      case 'workCategory':
      case 'phase':
      case 'building':
        this.service.GetBaseBuildingDetails(node.data.id, node.type).subscribe((result) => {
            this.details = result;

            if(this.details.startDate != null) {

              this.service.GetDateRanges(node.data.id, node.type).subscribe((result) => {

                this.events = result.items;
                this.schedule.gotoDate(new Date(this.details.startDate));

              });

            }
        });

        break;
    }
  }

  onRowSelected(building : Building){
    this.showTree = true;
    this.building = building;

    this.initTree();
    this.reloadPhases();

    this.selectedNode = null;
  }

  edit() {
    switch (this.selectedNode.type) {
      case 'work':
        this.workComponent.showEditDialog(this.selectedNode.data.id, this.selectedNode.data.workCategoryId);
        break;
      case 'workCategory':
        this.workCategoryComponent.displayEditDialog(this.selectedNode.data.id);
        break;
      case 'phase':
        this.phaseComponent.displayEditDialog(this.selectedNode.data.id);
        break;
      case 'building':
        this.crudComponent.showEditForm(this.selectedNode.data.id);
        break;
    }
  }

  showWorkResources(){
    this.workResoucesComponent.showForm(this.selectedNode.data.id);
  }
}
