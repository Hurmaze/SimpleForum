import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class StatisticService {
  readonly BaseURL = 'http://localhost:44387/api/statistics/';
  constructor(private http: HttpClient) { }
}
