import {Component, ElementRef, ViewChild, AfterViewInit, OnDestroy, Input} from '@angular/core';
import {LayoutService} from "../layout.service";
declare var jQuery: any;

@Component({
  selector: 'app-rightpanel',
  templateUrl: './rightpanel.component.html'
})
export class RightPanelComponent implements AfterViewInit, OnDestroy {

  rightPanelMenuScroller: HTMLDivElement;

  @Input() shared:LayoutService;

  @ViewChild('rightPanelMenuScroller') rightPanelMenuScrollerViewChild: ElementRef;

  constructor() {}

  ngAfterViewInit() {
    this.rightPanelMenuScroller = <HTMLDivElement> this.rightPanelMenuScrollerViewChild.nativeElement;

    setTimeout(() => {
      jQuery(this.rightPanelMenuScroller).nanoScroller({flash: true});
    }, 10);
  }

  ngOnDestroy() {
    jQuery(this.rightPanelMenuScroller).nanoScroller({flash: true});
  }
}
