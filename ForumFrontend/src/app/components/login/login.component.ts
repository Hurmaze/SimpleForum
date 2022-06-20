import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserAccountService } from 'src/app/shared/user-account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public email: string='';
  public password: string='';

  constructor(private service: UserAccountService, private router: Router) { }

  login(){
    this.service.login(this.email, this.password).subscribe(
      result => {},
      (error : HttpErrorResponse) => console.log(error.error)
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
