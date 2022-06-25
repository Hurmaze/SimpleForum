import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { FORUM_THREADS_API_URL } from '../app-injection';
import { ForumThread } from '../models/forum-thread.model';
import { Post } from '../models/post.model';

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
}
