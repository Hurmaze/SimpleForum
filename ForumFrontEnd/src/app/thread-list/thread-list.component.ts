import { Component, OnInit } from '@angular/core';
import { ForumThread } from '../models/forum-thread.model';
import { ForumThreadService } from '../shared/forum-thread.service';

@Component({
  selector: 'app-thread-list',
  templateUrl: './thread-list.component.html',
  styleUrls: ['./thread-list.component.css']
})
export class ThreadListComponent implements OnInit {
  public forumThreads: ForumThread[];

  constructor(private service: ForumThreadService) { }

  ngOnInit(): void {
  }

}
