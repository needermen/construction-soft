import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {CommonModule} from "@angular/common";
import {FooterComponent} from "./layout/footer/footer.component";
import {TopbarComponent} from "./layout/topbar/topbar.component";
import {RightPanelComponent} from "./layout/rightpanel/rightpanel.component";
import {MenuComponent, SubMenuComponent} from "./layout/menu/menu.component";
import {LayoutService} from "./layout/layout.service";
import {BrowserModule} from "@angular/platform-browser";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {CrudComponent} from './components/crud/crud.component';
import {DisableControlDirective} from "./directives/disable-control";
import {PrimeNgModule} from "./modules/prime-ng.module";

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,

    PrimeNgModule
  ],
  exports: [
    PrimeNgModule,

    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    FooterComponent,
    TopbarComponent,
    RightPanelComponent,
    MenuComponent,
    SubMenuComponent,

    CrudComponent,
    DisableControlDirective,
  ],
  declarations: [FooterComponent, TopbarComponent, RightPanelComponent, MenuComponent, SubMenuComponent, CrudComponent, CrudComponent, DisableControlDirective],
  providers: [LayoutService]
})
export class SharedModule {
}
