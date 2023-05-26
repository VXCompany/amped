import { Injectable } from '@angular/core';
import { mergeMap, Observable, of } from 'rxjs';
import { environment as env } from '../../../environments/environment';
import { ApiResponseModel, BookmarkModel, RequestConfigModel } from '../models';
import { ExternalApiService } from './external-api.service';

@Injectable({
  providedIn: 'root'
})
export class BookmarkService {
  constructor(public externalApiService: ExternalApiService) {}

  getFeed = (): Observable<ApiResponseModel> => {
    const config: RequestConfigModel = {
      url: `${env.api.bookmarkUrl}/bookmark`,
      method: 'GET',
      options: {
        headers: {
        'content-type': 'application/json',
        },
      }
    };

    return this.externalApiService.callExternalApi(config).pipe(
      mergeMap((response) => {
        const { data, error } = response;

        return of({
          data: data ? (data as BookmarkModel[]) : null,
          error,
        });
      })
    );
  };

  addBookmark = (uri: string): Observable<ApiResponseModel> => {
    const config: RequestConfigModel = {
      url: `${env.api.bookmarkUrl}/bookmark`,
      method: 'POST',
      options: {
        headers: {
          'content-type': 'application/json',
        },
        body: `{"uri":"${uri}"}`,
      }
    };

    return this.externalApiService.callExternalApi(config).pipe(
      mergeMap((response) => {
        const { data, error } = response;

        return of({
          data: data ? (data as BookmarkModel) : null,
          error,
        });
      })
    );
  }; 
}
