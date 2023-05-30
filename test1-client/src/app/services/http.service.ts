import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  baseUrl = 'https://localhost:7193/api/'

  constructor(private http: HttpClient) { }

  get(url: string): Observable<any> {
    return this.http.get<any>(this.baseUrl + url);
  }

  post(url: string, request: any) {
    return this.http.post<any>(this.baseUrl + url, request);
  }


}
