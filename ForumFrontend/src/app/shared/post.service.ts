import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { POSTS_API_URL } from '../app-injection';
import { Post } from '../models/post.model';
import { ACCES_TOKEN } from './user-account.service';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private options = { headers: new HttpHeaders().set('Content-Type', 'application/json'),
                      "Authorization":("Bearer " + localStorage.getItem(ACCES_TOKEN)) };
  constructor(
    private http: HttpClient,
    private router: Router,
    @Inject(POSTS_API_URL) private postsUrl: string
  ) { }

  get(): Observable<Post[]>{
    return this.http.get<Post[]>(this.postsUrl, this.options);
  }

  post(postModel: Post): Observable<Post>{

    return this.http.post<Post>(this.postsUrl,{
      AuthorEmail: postModel.authorEmail,
      AuthorId: postModel.authorId,
      Content: postModel.content,
      ThreadId: postModel.threadId,
      TimeCreated: postModel.timeCreated
      }, this.options);
  }
}