import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { CdbResponseModel } from '../models/cdb.response.model';
import { CdbViewModel } from './../models/cdb.view.model';

@Injectable({
  providedIn: 'root'
})
export class TesteB3Api {
  url: string = 'https://localhost:44322/Calculator/cdb';

  constructor(private http: HttpClient) {
  }

  getAllHousingLocations(cdbViewModel: CdbViewModel): Observable<CdbResponseModel> {
    return this.http.post<CdbResponseModel>(this.url, cdbViewModel);
  }
}