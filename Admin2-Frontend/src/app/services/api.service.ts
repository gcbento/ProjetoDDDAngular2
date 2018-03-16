import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import "rxjs/add/operator/map";

import { GenericResult } from './../models/models';

@Injectable()
export class ApiService {

  private _baseUrl: string = "http://localhost:4500/api";

  constructor(private _http: Http) { }

  public list(url: string, obj: Object): Observable<GenericResult<any>> {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');

    return this._http.post(this._baseUrl + url, JSON.stringify(obj), { headers: headers })
      .map(res => res.json());
  }

  public get(url: string): Observable<GenericResult<any>> {
    return this._http.get(this._baseUrl + url)
      .map(res => res.json());
  }

  public post(url: string, obj: Object): Observable<GenericResult<any>> {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');

    return this._http.post(this._baseUrl + url, JSON.stringify(obj), { headers: headers })
      .map(res => res.json());
  }

}


