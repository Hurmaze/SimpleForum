import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { concatMap } from 'rxjs';
import { Registration } from 'src/app/models/registration.model';
import { TokenService } from 'src/app/shared/token.service';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  public registrationModel: Registration = new Registration('','','','user', '');

  public emailControl = new FormControl();
  public passwordControl = new FormControl();
  public roleNameControl = new FormControl();
  public nicknameControl = new FormControl();

  public isError: boolean=false;
  public errorMessage: string='';

  constructor(
    private userService: UserService,
    private tokenService: TokenService,
    private router:Router) { }

  passwordsEqual():boolean{
    return this.registrationModel.Password === this.registrationModel.PasswordRepeat;
  }

  register(){
    this.userService.register(this.registrationModel)
    .pipe(concatMap(()=>this.tokenService.login(this.registrationModel.Email, this.registrationModel.Password)))
    .subscribe(
      result => {this.router.navigate(['/'])},
      (err : HttpErrorResponse) => {this.isError = true,
      this.errorMessage = err.error.ErrorMessage}
    );
  }

  isAuthenticated(){
    return this.tokenService.isAuthenticated();
  }

  ngOnInit(): void {
  }

}
