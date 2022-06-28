import { Component, OnInit } from '@angular/core';
import { concatMap } from 'rxjs';
import { ForumThread } from 'src/app/models/forum-thread.model';
import { Theme } from 'src/app/models/theme.model';
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

  isHiddenCreation = true;
  isHiddenUpdation = true;

  newThread: ForumThread=new ForumThread();
  selectedTheme:Theme=new Theme(0,'',[]);
  themes: Theme[]=[];

  constructor(
    private forumThreadService: ForumThreadService,
    private authService: UserAccountService) { }

  ngOnInit(): void {
    this.forumThreadService.get()
    .subscribe(result => this.threads=result);

    this.authService.get()
    .subscribe(result => this.authors=result);

    this.forumThreadService.getThemes()
    .subscribe(result => this.themes = result);
  }

  toggleCreation(){
    this.isHiddenCreation = !this.isHiddenCreation;
  }

  toggleUpdation(){
    this.isHiddenUpdation = !this.isHiddenUpdation;
  }

  postThread(){
    this.newThread.authorId=this.authService.currentUser().id;
    this.newThread.timeCreated = new Date();
    this.newThread.themeId = this.selectedTheme.id;
    this.forumThreadService.postThread(this.newThread).subscribe(res => {}, err => console.log(err));
  }

  updateThread(thread: ForumThread){
    this.forumThreadService.updateThread(thread).subscribe(res => {}, err => console.log(err));
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
