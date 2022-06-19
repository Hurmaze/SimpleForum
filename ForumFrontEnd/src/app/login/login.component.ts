import { Component, OnInit } from '@angular/core';
import { Login } from '../models/login.model';
import { UserAccountService } from '../shared/user-account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public LoginModel: Login;

  constructor(private service: UserAccountService) { }

  ngOnInit(): void {
  }

}
