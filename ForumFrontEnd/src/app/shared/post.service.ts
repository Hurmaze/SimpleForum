import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  readonly BaseURL = 'http://localhost:44387/api/posts/';
  constructor(private http: HttpClient) { }
}
