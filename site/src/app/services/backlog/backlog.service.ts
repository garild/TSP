import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BacklogItem } from 'src/app/models';


@Injectable({
  providedIn: 'root'
})

export class BacklogService {

  constructor(private httpclient: HttpClient) { }
  Create(backlogItem: BacklogItem) {
    return this.httpclient.post(`/api/backlog/create`, JSON.stringify(backlogItem));
  }
}
