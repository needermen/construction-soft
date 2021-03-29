import {Component, OnInit} from '@angular/core';
import {FullPageLoadingService} from "./shared/services/fullPageLoading.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  loading: boolean = false;

  constructor(private fullpagelodingService: FullPageLoadingService) {
  }

  ngOnInit(): void {
    this.fullpagelodingService.loading.subscribe((value) => {
      this.loading = value;
    })
  }
}
