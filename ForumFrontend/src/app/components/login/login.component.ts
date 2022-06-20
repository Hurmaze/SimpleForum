import { Component, OnInit } from '@angular/core';
import { UserAccountService } from 'src/app/shared/user-account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private service: UserAccountService) { }

  ngOnInit(): void {
  }

}
