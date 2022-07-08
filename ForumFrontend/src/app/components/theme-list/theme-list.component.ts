import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Theme } from 'src/app/models/theme.model';
import { AccessService } from 'src/app/shared/access.service';
import { ForumThreadService } from 'src/app/shared/forum-thread.service';

@Component({
  selector: 'app-theme-list',
  templateUrl: './theme-list.component.html',
  styleUrls: ['./theme-list.component.css']
})
export class ThemeListComponent implements OnInit {
  themes: Theme[]=[];
  isHiddenCreation: boolean=true;
  newTheme: Theme=new Theme(0,'');

  constructor(
      private forumThreadService: ForumThreadService,
      private router: Router,
      private accessService: AccessService
    ) { }

  ngOnInit(): void {
    this.forumThreadService.getThemes().subscribe(res => this.themes = res,
      err => this.router.navigate(['/']));
  }

  isModeratable(){
    return this.accessService
  }

  toggleCreation(){
    this.isHiddenCreation = !this.isHiddenCreation;
  }

  createTheme(){
    this.forumThreadService.createTheme(this.newTheme).subscribe(res => window.location.reload(), err => console.log(err));
  }

  deleteTheme(id: number){
    this.forumThreadService.deleteTheme(id).subscribe(res => window.location.reload());
  }

}
