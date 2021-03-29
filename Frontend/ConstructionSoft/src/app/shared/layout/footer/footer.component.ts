import {Component, Input} from '@angular/core';
import {LayoutService} from "../layout.service";

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent {
  @Input() shared: LayoutService;
}
