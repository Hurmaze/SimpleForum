import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from 'src/app/models/login.model';
import { UserAccountService } from 'src/app/shared/user-account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public loginModel: Login= new Login('','');

  public emailControl = new FormControl();
  public passwordControl = new FormControl();

  public isError: boolean=false;
  public errorMessage: string='';

  constructor(private service: UserAccountService, private router: Router) { }

  login(){
    this.service.login(this.loginModel.Email, this.loginModel.Password).subscribe(
      result => {},
      (err : HttpErrorResponse) => {this.isError = true,
      this.errorMessage = err.error.ErrorMessage}
    )
  }

  isAuthenticated(){
    return this.service.isAuthenticated();
  }

  logout(){
    this.service.logout();
  }

  ngOnInit(): void {
  }

}
