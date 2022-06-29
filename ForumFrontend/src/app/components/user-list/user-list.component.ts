import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Role } from 'src/app/models/role.model';
import { User } from 'src/app/models/user.model';
import { UserAccountService } from 'src/app/shared/user-account.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: User[]=[];

  constructor(
    private userService: UserAccountService,
    private router: Router,
    private location: Location) { }

  ngOnInit(): void {
    this.userService.get().subscribe(res => this.users = res,
      err => this.router.navigate(['/']));
  }

  deleteUser(id: number){
    this.userService.deleteUser(id).subscribe(res => {this.location.back()})
  }
}
