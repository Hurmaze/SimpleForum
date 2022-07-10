import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
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
  public filteredThreads: ForumThread[]=this.threads;
  public authors: User[] = [];
  page: number = 1;
  threadsOnPage:number=5;

  search:string='';

  isHiddenCreation = true;
  isHiddenUpdation = true;

  newThread: ForumThread=new ForumThread();
  selectedThemeId:number=0;
  themes: Theme[]=[];

  constructor(
    private forumThreadService: ForumThreadService,
    private userService: UserService,
    private tokenService: TokenService,
    private accessService: AccessService,
    private changeDetection: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.forumThreadService.get()
    .subscribe(result => {
      this.threads=result, 
      this.filteredThreads = result
    });

    this.userService.get()
    .subscribe(result => this.authors=result);

    this.forumThreadService.getThemes()
    .subscribe(result => this.themes = result);
  }

  Search() {
    if(this.filteredThreads.length === 0 || this.search === ''){
      this.filteredThreads = this.threads;
      this.changeDetection.detectChanges();
    }
    else{
      this.filteredThreads = this.threads;
      this.search = this.search.toLowerCase();
      this.filteredThreads = this.filteredThreads.filter((thread)=>{
        return thread.content.toLowerCase().includes(this.search) || 
        thread.title.toLowerCase().includes(this.search) ||
        thread.themeName.toLowerCase().includes(this.search);
      });
      this.changeDetection.detectChanges();
    }
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
