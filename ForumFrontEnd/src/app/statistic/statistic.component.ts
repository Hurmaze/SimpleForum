import { Component, OnInit } from '@angular/core';
import { ForumThread } from '../models/forum-thread.model';
import { User } from '../models/user.model';
import { StatisticService } from '../shared/statistic.service';

@Component({
  selector: 'app-statistic',
  templateUrl: './statistic.component.html',
  styleUrls: ['./statistic.component.css']
})
export class StatisticComponent implements OnInit {
  public users: User[];
  public forumThreads: ForumThread[];

  constructor(private servive: StatisticService) { }

  ngOnInit(): void {
  }

}
