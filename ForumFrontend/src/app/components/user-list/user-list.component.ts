import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.userService.get().subscribe(res => this.users = res,
      err => this.router.navigate(['/']));
  }

}
