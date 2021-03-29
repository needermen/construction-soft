import { Component, OnInit } from '@angular/core';
import {OrganizationService} from "./services/organization.service";
import {RoleService} from "../roles/services/role.service";

@Component({
  selector: 'app-organizations',
  templateUrl: './organizations.component.html',
  styleUrls: ['./organizations.component.css']
})
export class OrganizationsComponent implements OnInit {

  cols: any[] = [
    { field : 'name', header: 'დასახელება', type: 'input', denyedit: true },
    { field : 'taxCode', header: 'საიდენტიფიკაციო კოდი', type: 'input'},
    { field : 'phoneNumber', header: 'ტელეფონი', type: 'input'},
    { field : 'legalAddress', header: 'იურიდიული მისამართი', type: 'input', hideFromTable: true },
    { field : 'actualAddress', header: 'ფაქტიური მისამართი', type: 'input', hideFromTable: true },
    { field : 'bank', header: 'ბანკი', type: 'input', hideFromTable: true },
    { field : 'bankCode', header: 'ბანკის კოდი', type: 'input', hideFromTable: true },
    { field : 'accountNumber', header: 'ანგარიშის ნომერი', type: 'input', hideFromTable: true },
    { field : 'agreementNumber', header: 'ხელშეკრულების ნომერი', type: 'input', hideFromTable: true },
    { field : 'agreementDate', header: 'ხელშეკრულების დადების თარიღი', type: 'date', hideFromTable: true },
    { field : 'director', header: 'დირექტორი', type: 'input', hideFromTable: true },
    { field : 'directorPhoneNumber', header: 'დირექტორის ტელეფონის ნომერი', type: 'input', hideFromTable: true },
    { field : 'roleIds', header: 'მოდულები', hideFromTable: true, type: 'multi', service: this.roleService, selectfield: 'name', optional: true },
    { field : 'logo', header: 'ლოგო', type: 'logo' },
    { field : 'active', header: 'აქტიური', type: 'check'},
  ];

  title = 'ორგანიზაციები';
  detailTitle = 'ორგანიზაციის დეტალები';

  constructor(public service: OrganizationService, public roleService: RoleService) { }

  ngOnInit() {
  }

}
