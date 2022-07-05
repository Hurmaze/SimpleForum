import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Role } from 'src/app/models/role.model';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.css']
})
export class RoleListComponent implements OnInit {

  roles: Role[]=[];

  constructor(
      private router: Router,
      private userService: UserService
    ) { }

  ngOnInit(): void {
    this.userService.getRoles().subscribe(res => this.roles = res,
      err => this.router.navigate(['/']));
  }
}
