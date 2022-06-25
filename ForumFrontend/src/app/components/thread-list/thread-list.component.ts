import { Component, OnInit } from '@angular/core';
import { concatMap } from 'rxjs';
import { ForumThread } from 'src/app/models/forum-thread.model';
import { User } from 'src/app/models/user.model';
import { ForumThreadService } from 'src/app/shared/forum-thread.service';
import { UserAccountService } from 'src/app/shared/user-account.service';

@Component({
  selector: 'app-thread-list',
  templateUrl: './thread-list.component.html',
  styleUrls: ['./thread-list.component.css']
})
export class ThreadListComponent implements OnInit {
  public threads: ForumThread[] = [];
  public authors: User[] = [];

  constructor(
    private forumThreadService: ForumThreadService,
    private authService: UserAccountService) { }

  ngOnInit(): void {
    this.forumThreadService.get()
    .subscribe(result => this.threads=result);

    this.authService.get()
    .subscribe(result => this.authors=result);
  }

  isAuthenticated(){
    return this.authService.isAuthenticated();
  }

  isAllowedToChange(){
    let role = this.authService.getUserRole();
    role = role.toLowerCase()
    return "admin" === role || role === "moderator";
  }
}
