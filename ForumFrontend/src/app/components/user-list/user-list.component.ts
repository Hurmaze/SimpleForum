import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: User[]=[];
  roleId: number=0;
  isEmpty: boolean=false;

  constructor(
    private userService: UserService,
    private router: Router,
    private location: Location,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => this.roleId = params['roleId']);
    if(this.roleId!==undefined){
      this.userService.getByRole(this.roleId).subscribe(res => 
        {
          this.users = res;
          if(this.users.length===0){
            this.isEmpty= true;
          }
        },
        err => this.router.navigate(['/']));
    }
    else{
      this.userService.get().subscribe(res => this.users = res,
        err => this.router.navigate(['/']));
    }
  }

  deleteUser(id: number){
    this.userService.deleteUser(id).subscribe(res => {this.location.back()})
  }
}
