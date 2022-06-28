import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Theme } from 'src/app/models/theme.model';
import { ForumThreadService } from 'src/app/shared/forum-thread.service';
import { UserAccountService } from 'src/app/shared/user-account.service';

@Component({
  selector: 'app-theme-list',
  templateUrl: './theme-list.component.html',
  styleUrls: ['./theme-list.component.css']
})
export class ThemeListComponent implements OnInit {
  themes: Theme[]=[];

  constructor(
      private forumThreadService: ForumThreadService,
      private router: Router,
      private authService: UserAccountService
    ) { }

  ngOnInit(): void {
    this.forumThreadService.getThemes().subscribe(res => this.themes = res,
      err => this.router.navigate(['/']));
  }

  isAllowedToChange(){
    let role = this.authService.getUserRole();
    role = role.toLowerCase()
    return "admin" === role || role === "moderator";
  }
}
