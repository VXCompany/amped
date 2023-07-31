import {importProvidersFrom} from '@angular/core';
import {AppComponent} from './app/app.component';
import {bootstrapApplication, BrowserModule} from '@angular/platform-browser';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {PreloadAllModules, provideRouter, withPreloading} from '@angular/router';
import {AuthHttpInterceptor, AuthModule} from '@auth0/auth0-angular';
import {ROUTES} from './app/routes';
import {environment} from './environments/environment';

bootstrapApplication(AppComponent, {
    providers: [{
        provide: HTTP_INTERCEPTORS, useClass: AuthHttpInterceptor, multi: true,
    }, importProvidersFrom(BrowserModule, AuthModule.forRoot({
        ...environment.auth0, httpInterceptor: {
            allowedList: [
                {
                    uri: `${environment.api.bookmarkUrl}/bookmark`,
                    httpMethod: "POST"
                },
                `${environment.api.profileUrl}/profile`,
                `${environment.api.functionUrl}/api/GetUrlInfo`],
        }, cacheLocation: 'localstorage',
    }), HttpClientModule), provideRouter(ROUTES, withPreloading(PreloadAllModules), //withDebugTracing(),
    ),]
}).catch(err => console.error(err));

