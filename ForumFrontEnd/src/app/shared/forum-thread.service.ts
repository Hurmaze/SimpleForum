import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class ForumThreadService {
  readonly BaseURL = 'http://localhost:44387/api/forumthreads/';
  constructor(private http: HttpClient) { }
}
