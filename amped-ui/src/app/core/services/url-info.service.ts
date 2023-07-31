import { Injectable } from '@angular/core';
import { mergeMap, Observable, of } from 'rxjs';
import { environment as env } from '../../../environments/environment';
import { ApiResponseModel, UrlInfoModel, RequestConfigModel } from '../models';
import { ExternalApiService } from './external-api.service';

@Injectable({
  providedIn: 'root'
})
export class UrlInfoService {
  constructor(public externalApiService: ExternalApiService) {}

  getInfo = (url: string): Observable<ApiResponseModel> => {
    const config: RequestConfigModel = {
      url: `${env.api.functionUrl}/api/GetUrlInfo?Url=${url}`,
      method: 'GET',
      options: {
        headers: {
          'content-type': 'application/json',
          'x-functions-key': env.api.functionKey,
        },
      }
    };

    return this.externalApiService.callExternalApi(config).pipe(
      mergeMap((response) => {
        const { data, error } = response;

        return of({
          data: data ? (data as UrlInfoModel[]) : null,
          error,
        });
      })
    );
  };
}
