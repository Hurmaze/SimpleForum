import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ForumThread } from 'src/app/models/forum-thread.model';
import { Post } from 'src/app/models/post.model';
import { ForumThreadService } from 'src/app/shared/forum-thread.service';
import { PostService } from 'src/app/shared/post.service';
import { UserAccountService } from 'src/app/shared/user-account.service';

@Component({
  selector: 'app-forum-thread',
  templateUrl: './forum-thread.component.html',
  styleUrls: ['./forum-thread.component.css']
})
export class ForumThreadComponent implements OnInit {
  private id: number=0;

  thread: ForumThread= new ForumThread();
  posts: Post[]=[];

  postContent: string='';
  
  constructor(
    private threadService: ForumThreadService,
    private authService: UserAccountService,
    private postService: PostService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(routeParams => {
      this.loadPage(routeParams['id'])
      this.id = routeParams['id'];
    });
  }

  private loadPage(id: number){
    this.threadService.getById(id).subscribe(thread => this.thread = thread,
      err => this.router.navigate(['/']));
  }

  isAuthenticated(){
    return this.authService.isAuthenticated();
  }

  toBottom(){
    window.scrollTo(0,document.body.scrollHeight);
  }

  post(){
    let userToken = this.authService.currentUser();
    let postModel = new Post();
    postModel.authorId =userToken.id;
    postModel.content= this.postContent;
    postModel.threadId= this.id;
    postModel.timeCreated= new Date();
    postModel.authorEmail = userToken.email;

    this.postService.post(postModel).subscribe(res => window.location.reload(), err => err);
  }

}
