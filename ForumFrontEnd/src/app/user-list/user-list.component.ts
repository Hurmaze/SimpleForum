import { Component, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { UserAccountService } from '../shared/user-account.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  public users: User[];

  constructor(private service: UserAccountService) { }

  ngOnInit(): void {
  }

}
