import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';

import {LoginComponent} from './login/login.component';
import {AuthGuard} from "./guards/auth-guard.service";
import {LogoutComponent} from "./logout/logout.component";
import {AuthRequiredGuard} from "./guards/auth-required-guard.service";

const authRoutes: Routes = [
  {
    path: 'auth', children: [
      {path: '', redirectTo: 'login', pathMatch: 'full'},
      {path: 'login', component: LoginComponent, canActivate: [AuthGuard]},
      {path: 'logout', component: LogoutComponent, canActivate: [AuthRequiredGuard]},
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(authRoutes)
  ],
  exports: [RouterModule]
})
export class AuthRoutingModule {
}
