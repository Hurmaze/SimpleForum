import { Component, OnInit } from '@angular/core';
import { concatMap } from 'rxjs';
import { ForumThread } from 'src/app/models/forum-thread.model';
import { Theme } from 'src/app/models/theme.model';
import { User } from 'src/app/models/user.model';
import { AccessService } from 'src/app/shared/access.service';
import { ForumThreadService } from 'src/app/shared/forum-thread.service';
import { TokenService } from 'src/app/shared/token.service';
import { UserService } from 'src/app/shared/user.service';

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
  selectedThemeId:number=0;
  themes: Theme[]=[];

  constructor(
    private forumThreadService: ForumThreadService,
    private userService: UserService,
    private tokenService: TokenService,
    private accessService: AccessService) { }

  ngOnInit(): void {
    this.forumThreadService.get()
    .subscribe(result => this.threads=result);

    this.userService.get()
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
    this.newThread.authorId=this.tokenService.currentUser().id;
    this.newThread.themeId = this.selectedThemeId;
    this.forumThreadService.postThread(this.newThread).subscribe(res => { window.location.reload()}, err => console.log(err));
  }

  isAuthenticated(){
    return this.tokenService.isAuthenticated();
  }

  isModeratable(){
    return this.accessService.isModeratable();
  }

  deleteThread(id: number){
    this.forumThreadService.deleteThread(id).subscribe(res => window.location.reload());
  }
}
