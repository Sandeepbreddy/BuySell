import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImagesService {
  imageUrls = [];

  baseUrl = 'http://localhost:5000/api/images/';
  constructor(private http: HttpClient) {}

  getImages(model: string): Observable<any> {
    return this.http.get(this.baseUrl + 'loadimages', {
      params: {
        name: model
      }
    });
  }
}
