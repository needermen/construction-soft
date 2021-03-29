import {Component, OnInit, ViewChild} from '@angular/core';
import { UserService } from "./services/user.service";
import { OrganizationService } from "../organizations/services/organization.service";
import {UserRoleService} from "./services/user-role.service";
import {CrudComponent} from "../../../shared/components/crud/crud.component";
import {User} from "./models/user";

@Component({
  selector: 'app-organizations',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  cols: any[] = [
    { field : 'organizationId', header: 'ორგანიზაცია', type: 'select', service: this.organizationService, showfield:'organizationName', selectfield: 'name', optional: true },
    { field : 'fullName', header: 'სახელი და გვარი', type: 'input', denyedit: true },
    { field : 'userName', header: 'UserName', type: 'input', denyedit: true},
    { field : 'phoneNumber', header: 'ტელეფონი', type: 'input'},
    { field : 'personalId', header: 'პირადი ნომერი', type: 'input', denyedit: true},
    { field : 'roleIds', header: 'როლები', hideFromTable: true, type: 'multi', service: this.userRoleService, selectfield: 'name', dependson: true, optional: true },
    { field : 'active', header: 'აქტიური', type: 'check'}
  ];

  title = 'მომხმარებლები';
  detailTitle = 'მომხმარებლის დეტალები';

  @ViewChild(CrudComponent)
  private crudComponent: CrudComponent<User>;

  constructor(public service: UserService, public organizationService: OrganizationService, public userRoleService: UserRoleService) { }

  ngOnInit() {
  }

  onSelectChanged(change: any){
    if(change.col.field == 'organizationId'){
      if(change.value != null) {
        this.userRoleService.setOrganization(change.value.id);

        var col = this.crudComponent.getColByFieldName("roleIds");
        this.crudComponent.loadMulti(col);

        this.userRoleService.setOrganization(null);
      }
    }
  }

}
