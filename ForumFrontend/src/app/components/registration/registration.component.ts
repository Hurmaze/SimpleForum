import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { concatMap } from 'rxjs';
import { Registration } from 'src/app/models/registration.model';
import { UserAccountService } from 'src/app/shared/user-account.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  public registrationModel: Registration=new Registration('','','','user', '');

  public emailControl = new FormControl();
  public passwordControl = new FormControl();
  public roleNameControl = new FormControl();
  public nicknameControl = new FormControl();

  public isError: boolean=false;
  public errorMessage: string='';

  constructor(private service: UserAccountService, private router:Router) { }

  passwordsEqual():boolean{
    return this.registrationModel.Password === this.registrationModel.PasswordRepeat;
  }

  register(){
    this.service.register(this.registrationModel)
    .pipe(concatMap(()=>this.service.login(this.registrationModel.Email, this.registrationModel.Password)))
    .subscribe(
      result => {this.router.navigate(['/'])},
      (err : HttpErrorResponse) => {this.isError = true,
      this.errorMessage = err.error.ErrorMessage}
    );
  }

  isAuthenticated(){
    return this.service.isAuthenticated();
  }

  ngOnInit(): void {
  }

}
