import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../../auth/services/auth.service";
import {MessageService} from "primeng/api";

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  form: FormGroup;
  errorMessage: string;

  constructor(private authService: AuthService, private messageService: MessageService) {
  }

  ngOnInit() {
    this.form = new FormGroup({
      oldPassword: new FormControl('', Validators.compose([Validators.required, Validators.minLength(6)])),
      newPassword: new FormControl('', Validators.compose([Validators.required, Validators.minLength(6)])),
      repeatNewPassword: new FormControl('', Validators.compose([Validators.required, Validators.minLength(6)]))
    });
  }

  submit() {
    if (this.isValid()) {
      this.authService.changePassword(this.oldPassword.value, this.newPassword.value).subscribe((result) => {
        if(result){
          this.messageService.add({ severity:'success', detail: 'პაროლი წარმატებით განახლდა'});
          this.form.reset();
        }
      });
    }
  }

  isValid() {
    if (this.form.valid && this.form.get('newPassword').value == this.form.get('repeatNewPassword').value) {
      return true;
    } else {
      this.errorMessage = 'პაროლები არ ემთხვევა';
      return false;
    }
  }

  get oldPassword() {
    return this.form.get('oldPassword')
  };

  get newPassword() {
    return this.form.get('newPassword')
  };

  get repeatNewPassword() {
    return this.form.get('repeatNewPassword')
  };
}
