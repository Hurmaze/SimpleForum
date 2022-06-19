import { Component, OnInit } from '@angular/core';
import { Registration } from '../models/registration.model';
import { UserAccountService } from '../shared/user-account.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  public registration: Registration;

  constructor(private service: UserAccountService) { }

  ngOnInit(): void {
  }

}
