import { NgModule } from '@angular/core';

import { LoginComponent } from './login/login.component';
import { AuthRoutingModule } from './auth-routing.module';
import {AuthService} from './services/auth.service';
import {SharedModule} from "../shared/shared.module";
import {AuthGuard} from "./guards/auth-guard.service";
import {AuthRequiredGuard} from "./guards/auth-required-guard.service";
import { LogoutComponent } from './logout/logout.component';

@NgModule({
  declarations: [
    LoginComponent,
    LogoutComponent,
  ],
  imports: [
    SharedModule,
    AuthRoutingModule
  ],
  providers: [
    AuthService,

    AuthGuard,
    AuthRequiredGuard
  ]
})
export class AuthModule {}
