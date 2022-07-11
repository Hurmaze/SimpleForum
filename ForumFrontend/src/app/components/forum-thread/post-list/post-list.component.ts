import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Post } from 'src/app/models/post.model';
import { AccessService } from 'src/app/shared/access.service';
import { ForumThreadService } from 'src/app/shared/forum-thread.service';
import { PostService } from 'src/app/shared/post.service';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.css']
})
export class PostListComponent implements OnInit {
  threadId:number=0;
  posts: Post[]=[];
  page: number=1;
  postsOnPage: number=5;

  constructor(
    private threadsService: ForumThreadService,
     private route: ActivatedRoute,
     private postService: PostService,
     private accessSerivce: AccessService,
     private changeDetection: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.route.params.subscribe(routeParams => {
      this.loadPage(routeParams['id'])
    });
  }

  private loadPage(threadId: number){
    this.threadsService.getThreadPosts(threadId)
    .subscribe(res => this.posts = res);
  }

  isModeratable(){
    return this.accessSerivce.isModeratable();
  }

  deletePost(id: number){
    this.postService.deletePost(id).subscribe(() => window.location.reload());
  }
}
