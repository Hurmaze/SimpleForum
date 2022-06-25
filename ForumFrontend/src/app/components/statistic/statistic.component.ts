import { Component, OnInit } from '@angular/core';
import { ForumThread } from 'src/app/models/forum-thread.model';
import { User } from 'src/app/models/user.model';
import { StatisticService } from 'src/app/shared/statistic.service';

@Component({
  selector: 'app-statistic',
  templateUrl: './statistic.component.html',
  styleUrls: ['./statistic.component.css']
})
export class StatisticComponent implements OnInit {
  users: User[] = [];
  threads: ForumThread[]=[];

  constructor(private service: StatisticService) { }

  ngOnInit(): void {
    this.service.getTopUsers(3).subscribe(res => this.users = res);
    this.service.getTopThreads(3).subscribe(res => this.threads = res);
  }

}
