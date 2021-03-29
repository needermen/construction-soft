import {Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild} from '@angular/core';
import {Subscription, timer} from "rxjs";
import {LazyLoadEvent} from "../../../../../node_modules/primeng/api";

import {FormBuilder, FormControl, FormGroup} from "@angular/forms";
import {ICrudService} from "./services/i-crud-service";
import {ServiceResult} from "../../models/serviceResult";
import {ListResult} from "../../models/listResult";

@Component({
  selector: 'app-crud',
  templateUrl: './crud.component.html',
  styleUrls: ['./crud.component.css']
})
export class CrudComponent<T> implements OnInit, OnDestroy {
  @Input() crudService: ICrudService<T>;
  subscription: Subscription;

  items: any[];

  // main param
  @Input() provideTable: boolean = true;

  // table info
  total: number;
  rows: number = 7;
  currentpage: number = 0;
  search: string = '';
  loading: boolean;

  // inputs
  @Input() cols: any[];
  @Input() title: string;
  @Input() detailTitle = 'კატეგორიის დეტალები';
  @Input() tableSummaryInfo: string;

  // select data
  selectData: any;
  selectSelected: any;
  @Output() selectChanged = new EventEmitter<any>();

  // multi data
  multiData: any;
  multiSelected: any;

  // edit form details
  newItem: boolean;
  item: any;
  displayDialog: boolean;

  // form
  form: FormGroup;

  @Input() overrideRowSelectEvent: boolean = false;
  @Output() rowSelected = new EventEmitter<T>();
  @Output() itemUpdateCompleted = new EventEmitter<void>();
  @Output() itemDeleted = new EventEmitter<void>();
  @Output() userInputed = new EventEmitter<any>();
  @Output() formShow = new EventEmitter<void>();
  @Output() formHide = new EventEmitter<void>();

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.fetchData();

    this.selectData = {};
    this.selectSelected = {};

    this.multiData = {};
    this.multiSelected = {};

    for(var col of this.cols){
      if(col.type == 'select' || col.type == 'dropdown') {
        this.selectData[col.field] = [];
        this.selectSelected[col.field] = {};
      }
      if(col.type == 'multi'){
        this.multiData[col.field] = [];
        this.multiSelected[col.field] = [];

        this.loadMulti(col);
      }
    }

    this.initForm();
  }

  ngOnDestroy(){
    if(this.subscription != null) {
      this.subscription.unsubscribe();
    }
  }


  //form

  initForm(){
    var formControls = {};
    for(var col of this.cols){
      formControls[col.field] = new FormControl('');
    }

    this.form = this.formBuilder.group(formControls);
  }

  formIsValid(){
    for(var col of this.cols) {
      if(!col.optional) {
        switch (col.type) {
          case 'select':
          case 'dropdown':
            if (this.selectSelected[col.field] == null || this.selectSelected[col.field].id == null)
              return false;
            break;
        }
      }
    }

    return true;
  }

  showEditForm(id){
    this.subscription = this.crudService.getById(id).subscribe((result) => {
      let item = result;

      for(var col of this.cols){
        this.fillData(col, item);
        if(col.type == 'date'){
          item[col.field] = new Date(item[col.field]);
        }
      }

      this.newItem = false;
      this.item = this.clone(item);
      this.displayDialog = true;
    });
  }

  showNewItemForm(item) {
    this.newItem = true;
    this.item = item == null ? {} : item;
    this.displayDialog = true;

    for (let col of this.cols){
      this.setSelectValue(col, {});
      this.item[col.field] = col.default;
    }

    this.selectSelected = {};
    this.multiSelected = {};
  }

  // events

  onRowSelect(event) {
    this.rowSelected.emit(event.data);
    if(!this.overrideRowSelectEvent){
      this.showEditForm(event.data.id);
    }
  }

  onAdd() {
    this.showNewItemForm(null);
  }

  onSelectValueChanged(col){
    this.selectChanged.emit({ col: col, value:  this.selectSelected[col.field] } );
    this.userInputed.emit({ col: col });
  }

  onDropDownValueChanged(col){
    this.userInputed.emit( { col: col});
  }

  onMultiValueChanged(col){
    this.userInputed.emit( { col: col});
  }

  onInputValueChanged(col){
    this.userInputed.emit( { col: col});
  }

  onCheckValueChanged(col){
    this.userInputed.emit( { col: col});
  }

  onDateValueChanged(col){
    this.userInputed.emit( { col: col});
  }

  onupload(event){
    let result: ServiceResult<object> = JSON.parse(event.xhr.response);
    if(result.success == true){
      if(this.item.files == null) {
        this.item.files = result.data.items;
      }else{
        this.item.files = [...this.item.files, ...result.data.items]
      }
    }
  }

  onLogoSelect(event, col){
    this.item[col.field] = event.files[0];
    this.fileTobase64(col.field);
  }

  // setters

  setSelectValue(col, value){
    this.selectSelected[col.field] = value;
    this.onSelectValueChanged(col);
  }


  // load data

  loadSelect(event, col){
    this.subscription = col.service.get(0, 100, event.query).subscribe((result)=>{
      this.selectData[col.field] = result.items;
      if(col.optional){
        let item = {id : null};
        item[col.selectfield] = 'არცერთი';
        this.selectData[col.field].splice(0, 0, item)
      }
    });
  }

  loadDropdown(col){
    this.subscription = col.service.get().subscribe((result)=>{
      this.selectData[col.field] = result.items;
      if(col.optional){
        let item = {id:null};
        item[col.selectfield] = 'არცერთი';
        this.selectData[col.field].splice(0, 0, item)
      }
    });
  }

  loadMulti(col){
    this.subscription = col.service.get().subscribe((result) => {
      this.multiData[col.field] = result.items;
    });

    this.multiSelected[col.field] = [];
  }


  // fetch and filter data

  fetchData(){
    if(this.provideTable) {
      this.loading = true;

      this.subscription = this.crudService.get(this.currentpage, this.rows, this.search).subscribe((result) => {
        this.items = result.items;
        this.total = result.total;

        this.loading = false;
      });
    }
  }

  filterData(value: string){
    this.search = value;
    this.currentpage = 0;

    this.fetchData();
  }

  fetchDataLazy(event: LazyLoadEvent) {
    this.currentpage = event.first / this.rows;
    this.fetchData();
  }


  // commands

  save() {
    this.displayDialog = false;

    let itemToSave = this.clone(this.item);

    for(var col of this.cols){
      switch (col.type) {
        case 'select':
        case 'dropdown':
          itemToSave[col.field] = this.selectSelected[col.field] == null ? null : this.selectSelected[col.field].id;
          break;
        case 'multi':
          itemToSave[col.field] = [];
          if(this.multiSelected[col.field] != null) {
            for (let selectedFromMulti of this.multiSelected[col.field]) {
              if (selectedFromMulti != null) {
                itemToSave[col.field].push(selectedFromMulti.id);
              }
            }
          }
          break;
        case 'date':
          let date = itemToSave[col.field] as Date;
          itemToSave[col.field] = date != null ? date.toDateString() : null;
          break;
      }
    }

    if (this.newItem)
      this.subscription = this.crudService.add(itemToSave).subscribe((result)=>{
        if(result > 0) {
          this.itemUpdateCompleted.emit();
          this.fetchData();

          this.item = null;
        } else {
          this.displayDialog = true;
        }
      });
    else
      this.subscription = this.crudService.update(itemToSave.id, itemToSave).subscribe((result)=>{
        if(result) {
          this.itemUpdateCompleted.emit();
          this.fetchData();

          this.item = null;
        } else {
          this.displayDialog = true;
        }
      });
  }

  delete() {
    this.displayDialog = false;

    this.subscription = this.crudService.delete(this.item.id).subscribe((result)=>{
      if(result){
        this.itemUpdateCompleted.emit();
        this.itemDeleted.emit();
        this.fetchData();
        this.item = null;
      } else {
        this.displayDialog = true;
      }
    });
  }


  // fill data

  fillData(col, data){
    //TODO call depended fields after main fields fill finishs

    timer(col.dependson? 500 : 0).subscribe(()=> {
      switch (col.type) {
        case 'select':
        case 'dropdown':
          this.fillSelect(col, data);
          break;
        case 'multi':
          this.fillMulti(col, data);
          break;
        case 'date':
          this.fillDate(col, data);
          break;
      }
    });
  }

  fillSelect(col, data){
    if(data[col.field] != null) {
      this.subscription = (col.fillService != null ? col.fillService : col.service).getById(data[col.field]).subscribe((result) => {
        this.setSelectValue(col, result);
      });
    }else {
      this.setSelectValue(col.field,{});
    }
  }

  fillMulti(col, data){
    if(data[col.field] != null) {
      this.multiSelected[col.field] = (this.multiData[col.field]).filter(function(element: any, index, array){
        return data[col.field].includes(element.id);
      });
    }else {
      this.multiSelected[col.field] = [];
    }
  }

  fillDate(col, data){
    let date = data[col.field] as Date;

    if(date == null || date.getFullYear() < 2000){
      this.item[col.field] = null;
    }
  }


  // helpers

  getTableColumsCount(){
    return this.cols.filter(function(col){
      return col.hideFromTable != true;
    }).length
  }

  denyfieldedit(col){
    return col.denyedit != null && col.denyedit && !this.newItem;
  }

  clone(c: any): any {
    let item =  {};
    for (let prop in c) {
      item[prop] = c[prop];
    }
    return item;
  }

  getProperty( propertyName, object ) {
    if(propertyName == undefined)
      return undefined;

    var parts = propertyName.split( "." ),
      length = parts.length,
      i,
      property = object || this;

    if(property == undefined)
      return undefined;

    for ( i = 0; i < length; i++ ) {
      property = property[parts[i]];
      if(property == undefined)
        return undefined;
    }

    return property;
  }

  getColByFieldName(name){
    return this.cols.filter(function (element) {
      return element.field == name;
    })[0];
  }

  //file to base 64

  fileTobase64(fieldName){
    this.base64FieldName = fieldName;

    if (this.item[fieldName]) {
      var reader = new FileReader();
      reader.onload = this._handleReaderLoaded.bind(this);
      reader.readAsBinaryString(this.item[fieldName]);
    }
  }

  _handleReaderLoaded(readerEvt) {
    var binaryString = readerEvt.target.result;
    this.item[this.base64FieldName] = btoa(binaryString);
  }

  base64FieldName: string;
}
