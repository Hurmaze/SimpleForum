import { Component, OnInit } from '@angular/core';
import { ForumThread } from '../models/forum-thread.model';
import { ForumThreadService } from '../shared/forum-thread.service';

@Component({
  selector: 'app-forum-thread',
  templateUrl: './forum-thread.component.html',
  styleUrls: ['./forum-thread.component.css']
})
export class ForumThreadComponent implements OnInit {
  public forumThread: ForumThread;
  
  constructor(private service: ForumThreadService) { }

  ngOnInit(): void {
  }

}
