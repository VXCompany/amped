import {Injectable} from '@angular/core';
import {mergeMap, Observable, of} from 'rxjs';
import {environment as env} from '../../../environments/environment';
import {AmpedProfileModel, ApiResponseModel, RequestConfigModel} from '../models';
import {ExternalApiService} from './external-api.service';
import {ConfigService} from "./config.service";

@Injectable({
    providedIn: 'root'
})
export class ProfileService {
    constructor(public externalApiService: ExternalApiService, private configService: ConfigService) {
    }

    getProfile = (): Observable<ApiResponseModel> => {
        const config: RequestConfigModel = {
            url: `${this.configService.getConfig()['profileUrl']}/profile`,
            method: 'GET',
            options: {
                headers: {
                    'content-type': 'application/json',
                },
            }
        };

        return this.externalApiService.callExternalApi(config).pipe(
            mergeMap((response) => {
                const {data, error} = response;

                return of({
                    data: data ? (data as AmpedProfileModel) : null,
                    error,
                });
            })
        );
    };

    createProfile = (nickname: string, bio: string): Observable<ApiResponseModel> => {
        const config: RequestConfigModel = {
            url: `${this.configService.getConfig()['profileUrl']}/profile`,
            method: 'POST',
            options: {
                headers: {
                    'content-type': 'application/json',
                },
                body: `{"nickname":"${nickname}", "bio":"${bio}"}`,
            }
        };

        return this.externalApiService.callExternalApi(config).pipe(
            mergeMap((response) => {
                const {data, error} = response;

                return of({
                    data: data ? (data as AmpedProfileModel) : null,
                    error,
                });
            })
        );
    };

    updateProfile = (nickname: string, bio: string): Observable<ApiResponseModel> => {
        const config: RequestConfigModel = {
            url: `${this.configService.getConfig()['profileUrl']}/profile`,
            method: 'PUT',
            options: {
                headers: {
                    'content-type': 'application/json',
                },
                body: `{"nickname":"${nickname}", "bio":"${bio}"}`,
            }
        };

        return this.externalApiService.callExternalApi(config).pipe(
            mergeMap((response) => {
                const {data, error} = response;

                return of({
                    data: data ? (data as AmpedProfileModel) : null,
                    error,
                });
            })
        );
    };
}
