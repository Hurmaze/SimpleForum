import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { UserAccountService } from 'src/app/shared/user-account.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  user: User=new User(0,'','',[],[],'');

  constructor(
    private authService: UserAccountService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(routeParams => {
      this.loadPage(routeParams['id'])
    });
  }

  isCurrentUser(){
    return this.user.email === this.authService.currentUser().email;
  }

  private loadPage(id: number){
    this.authService.getById(id)
    .subscribe(res => {this.user = res},
      err => {this.router.navigate(['/'])});
  }

}
