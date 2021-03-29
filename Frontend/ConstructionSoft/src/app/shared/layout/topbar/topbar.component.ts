import {Component, Input} from '@angular/core';
import {LayoutService} from "../layout.service";

@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html'
})
export class TopbarComponent {
  @Input() shared: LayoutService;
  @Input() user: string;

  constructor() {

  }

}
