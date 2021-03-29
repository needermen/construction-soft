import {Component, OnDestroy, OnInit} from '@angular/core';
import {AuthService} from "../services/auth.service";
import {Router} from "@angular/router";
import {Subscription} from "rxjs";

@Component({template:''})
export class LogoutComponent implements OnInit, OnDestroy {
  private subscription: Subscription;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.subscription = this.authService.logout().subscribe((result)=>{
      if(result) {
        this.authService.clearUser();
        this.router.navigateByUrl('auth/login');
      }
    });
  }

  ngOnDestroy(){
    this.subscription.unsubscribe();
  }

}
