import { Component, OnInit } from '@angular/core';
import { Theme } from '../models/theme.model';
import { ForumThreadService } from '../shared/forum-thread.service';

@Component({
  selector: 'app-theme-list',
  templateUrl: './theme-list.component.html',
  styleUrls: ['./theme-list.component.css']
})
export class ThemeListComponent implements OnInit {
  public themes: Theme[];

  constructor(private service: ForumThreadService) { }

  ngOnInit(): void {
  }

}
