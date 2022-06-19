import { Component, OnInit } from '@angular/core';
import { Role } from '../models/role.model';
import { UserAccountService } from '../shared/user-account.service';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.css']
})
export class RoleListComponent implements OnInit {
  public roles: Role[];

  constructor(private service: UserAccountService) { }

  ngOnInit(): void {
  }

}
