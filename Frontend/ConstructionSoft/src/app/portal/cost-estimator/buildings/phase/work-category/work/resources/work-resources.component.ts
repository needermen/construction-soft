import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Work} from "../models/work";
import {WorkService} from "../services/work.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {timer} from "rxjs";

@Component({
  selector: 'app-work-resources',
  templateUrl: './work-resources.component.html',
  styleUrls: ['./work-resources.component.css']
})
export class WorkResourcesComponent implements OnInit {
  show: boolean;

  work: Work;

  form: FormGroup;

  showErrors: boolean;

  @Output() closed = new EventEmitter<void>();

  constructor(private fb: FormBuilder, private workService: WorkService) {
  }

  ngOnInit() {
    this.form = this.fb.group({
      contractorPrice: ['', Validators.required],
      extraPricePercent: [''],
      executeByContractor: [''],
      contractorName: ['', Validators.required],

      contractorExtraPrice: ['']
    });
  }

  onPricePercentChange(event) {
    this.work.extraPricePercent = this.form.get('extraPricePercent').value;
  }

  onSubmit() {
    if (this.form.get('executeByContractor').value == true && this.form.invalid) {
      this.showErrors = true;
      timer(3000).subscribe(() => {
        this.showErrors = false;
      });
    }
    else {
      this.updateWork(true);
    }
  }

  onCheck(event) {
    if (event == false) {
      this.updateWork(false);
    }
  }

  //methods
  showForm(id) {
    this.work = null;
    this.loadWork(id);
  }

  updateWork(hideAfter) {
    this.workService.getById(this.work.id).subscribe((result) => {
      this.work = result;

      this.work.contractorPrice = this.form.get('contractorPrice').value;
      this.work.contractorExtraPrice = this.form.get('contractorExtraPrice').value;
      this.work.executeByContractor = this.form.get('executeByContractor').value;
      this.work.contractorName = this.form.get('contractorName').value;
      this.work.extraPricePercent = this.form.get('extraPricePercent').value;

      this.show = !hideAfter;
      this.workService.update(this.work.id, this.work).subscribe((result) => {
        if (!result) {
          this.show = true;
        }
      });
    });
  }

  loadWork(id) {
    this.workService.getById(id != null ? id : this.work.id).subscribe((result) => {
      this.work = result;

      this.form.get('contractorPrice').setValue(this.work.contractorPrice);
      this.form.get('contractorExtraPrice').setValue(this.work.contractorExtraPrice);
      this.form.get('executeByContractor').setValue(this.work.executeByContractor);
      this.form.get('contractorName').setValue(this.work.contractorName);
      this.form.get('extraPricePercent').setValue(this.work.extraPricePercent);

      this.show = true;
    });
  }

  get executeByContractor() {
    return this.form.get('executeByContractor');
  }

  get contractorName() {
    return this.form.get('contractorName');
  }

  get contractorPrice() {
    return this.form.get('contractorPrice');
  }

  get contractorExtraPrice() {
    return this.form.get('contractorExtraPrice');
  }

  get extraPricePercent() {
    return this.form.get('extraPricePercent');
  }
}
