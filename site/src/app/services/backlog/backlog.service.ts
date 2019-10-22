import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BacklogItem } from 'src/app/models';


@Injectable({
  providedIn: 'root'
})

export class BacklogService {

  constructor(private httpclient: HttpClient) { }
  baseUrl =""
  Create(backlogItem: BacklogItem) {
    return this.httpclient.post(`${this.baseUrl}/api/backlog/create`, JSON.stringify(backlogItem));
  }
}
