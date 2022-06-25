import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { STATISTICS_API_URL } from '../app-injection';
import { ForumThread } from '../models/forum-thread.model';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class StatisticService {
  private options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };
  constructor(
    private http: HttpClient,
    @Inject(STATISTICS_API_URL) private statisticsUrl: string,
    private router: Router
    ) { }

  getTopUsers(count: number): Observable<User[]>{
    return this.http.get<User[]>(`${this.statisticsUrl}users/active/${count}`, this.options);
  }

  getTopThreads(count: number): Observable<ForumThread[]>{
    return this.http.get<ForumThread[]>(`${this.statisticsUrl}threads/popular/${count}`, this.options);
  }
}
