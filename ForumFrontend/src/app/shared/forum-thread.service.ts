import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { FORUM_THREADS_API_URL } from '../app-injection';
import { ForumThread } from '../models/forum-thread.model';
import { Post } from '../models/post.model';
import { Theme } from '../models/theme.model';

@Injectable({
  providedIn: 'root'
})
export class ForumThreadService {
  private options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };

  constructor(
    private http: HttpClient,
    private router: Router,
    @Inject(FORUM_THREADS_API_URL) private forumThreadsUrl: string
    ) { }

  get(): Observable<ForumThread[]>{
    return this.http.get<ForumThread[]>(this.forumThreadsUrl, this.options);
  }

  getById(id: number): Observable<ForumThread>{
    return this.http.get<ForumThread>(`${this.forumThreadsUrl}${id}`)
  }

  getThreadPosts(id: number): Observable<Post[]>{
    return this.http.get<Post[]>(`${this.forumThreadsUrl}${id}/posts`);
  }

  getThemes():Observable<Theme[]>{
    return this.http.get<Theme[]>(`${this.forumThreadsUrl}themes`);
  }

  postThread(thread: ForumThread): Observable<ForumThread>{
    return this.http.post<ForumThread>(this.forumThreadsUrl,{
      Title: thread.title,
      Content: thread.content,
      ThemeId: thread.themeId,
      AuthorId: thread.authorId,
      TimeCreated: thread.timeCreated

    }, this.options);
  }

  updateThread(id:Number, thread: ForumThread): Observable<ForumThread>{
    return this.http.put<ForumThread>(`${this.forumThreadsUrl}${id}`,{
      Id: thread.id,
      Title: thread.title,
      Content: thread.content,
      ThemeId: thread.themeId,
      AuthorId: thread.authorId,
      TimeCreated: thread.timeCreated

    }, this.options);
  }

  deleteThread(id:number): Observable<any>{
    return this.http.delete(`${this.forumThreadsUrl}${id}`);
  }

  deleteTheme(id:number): Observable<any>{
    return this.http.delete(`${this.forumThreadsUrl}themes/${id}`);
  }

  createTheme(theme: Theme): Observable<Theme>{
    return this.http.post<Theme>(`${this.forumThreadsUrl}themes`,
    {
      Id: 0,
      ThemeName: theme.themeName,
      ForumThreads: []
    }
    ,this.options);
  }
}
