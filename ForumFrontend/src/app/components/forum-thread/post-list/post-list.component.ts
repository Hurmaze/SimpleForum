import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Post } from 'src/app/models/post.model';
import { ForumThreadService } from 'src/app/shared/forum-thread.service';
import { UserAccountService } from 'src/app/shared/user-account.service';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.css']
})
export class PostListComponent implements OnInit {
  threadId:number=0;
  posts: Post[]=[];

  constructor(
    private threadsService: ForumThreadService,
     private route: ActivatedRoute,
     private authService: UserAccountService) { }

  ngOnInit(): void {
    this.route.params.subscribe(routeParams => {
      this.loadPage(routeParams['id'])
    });
  }

  private loadPage(threadId: number){
    this.threadsService.getThreadPosts(threadId)
    .subscribe(res => this.posts = res);
  }

  isAllowedToChange(){
    let role = this.authService.getUserRole();
    role = role.toLowerCase()
    return "admin" === role || role === "moderator";
  }
}
