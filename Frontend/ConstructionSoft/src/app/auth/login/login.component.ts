import {Component, OnDestroy, OnInit} from '@angular/core';

import {AuthService} from '../services/auth.service';
import {Subscription, timer} from "rxjs";
import {Router} from "@angular/router";
import {UserModel} from "../services/models/userModel";
import {FullPageLoadingService} from "../../shared/services/fullPageLoading.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {
  private subscription: Subscription;

  errorMessage: string;
  username: string;
  password: string;

  loading: boolean = false;

  constructor(private authService: AuthService, private router: Router, private fullPageLoadingService: FullPageLoadingService) {
  }

  ngOnInit() {
  }

  onLogin() {
    this.fullPageLoadingService.setLoading(true);
    this.loading = true;

    timer(1000).subscribe(() =>
      this.subscription = this.authService.login(this.username, this.password)
        .subscribe(
          (user: UserModel) => {
            this.fullPageLoadingService.setLoading(false);
            this.loading = false;

            if (user != null && user.token != null) {
              this.authService.setUser(user);
              if(user.passwordShouldChange){
                this.router.navigateByUrl('/portal/account');
              }else {
                this.router.navigateByUrl('/portal');
              }
            }
          })
    );
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  clearErrorMessage() {
    this.errorMessage = '';
  }
}
