import {Component, OnInit} from '@angular/core';
import {LayoutService} from "../shared/layout/layout.service";
import {AuthService} from "../auth/services/auth.service";
import {UserModel} from "../auth/services/models/userModel";
import {RoleEnum} from "../auth/services/models/role-enum";

@Component({
  selector: 'app-portal',
  templateUrl: './portal.component.html'
})
export class PortalComponent implements OnInit {
  menuItems: any[] = [];

  adminItems: any[] = [
    {
      label: 'ადმინისტრირება', icon: '',
      items: [
        {label: 'ორგანიზაციები', icon: '', routerLink: 'admin/orgs'},
        {label: 'მომხმარებლები', icon: 'fa fa-fw fa-users', routerLink: 'admin/users'},
      ]
    }
  ];

  resourceManagerItems: any[] = [
    {
      label: 'რესურსების მართვა', icon: '',
      items: [
        {
          label: 'მასალები და ტექნიკა', icon: '',
          items: [
            {label: 'სამშენებლო მასალები', icon: '', routerLink: 'admin/materials/building'},
            {label: 'სახარჯი მასალები', icon: '', routerLink: 'admin/materials/consumption'},
            {label: 'ძირითადი მასალები', icon: '', routerLink: 'admin/materials/main'},
            {label: 'ტექნიკა', icon: '', routerLink: 'admin/technics'},
          ]
        },
        {
          label: 'მუშახელი და ბრიგადები', icon: '',
          items: [
            {label: 'მუშახელები', icon: '', routerLink: 'admin/hr/workers'},
            {label: 'ბრიგადები', icon: '', routerLink: 'admin/hr/brigades'},
          ]
        }
      ]
    }
  ];

  costEstimatorItems: any[] = [
    {
      label: 'ობიექტები', routerLink: 'cost-estimator/buildings'
    }
  ];

  user: UserModel;

  constructor(public layout: LayoutService, private authService: AuthService) {

  }

  ngOnInit() {
    this.user = this.authService.getUser();

    for (var role of this.user.roleIds) {
      switch (role) {
        case RoleEnum.CostEstimator:
          this.menuItems = [...this.menuItems, ...this.costEstimatorItems];
          break;
        case RoleEnum.Admin:
          this.menuItems = [...this.menuItems, ...this.adminItems];
          break;
        case RoleEnum.ResourceManager:
          this.menuItems = [...this.menuItems, ...this.resourceManagerItems];
          break;
      }
    }
  }
}
