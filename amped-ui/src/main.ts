import {APP_INITIALIZER} from '@angular/core';
import {AppComponent} from './app/app.component';
import {bootstrapApplication} from '@angular/platform-browser';
import {
    HttpBackend,
    HttpClient,
    provideHttpClient,
    withInterceptors
} from "@angular/common/http";
import {PreloadAllModules, provideRouter, withPreloading} from '@angular/router';
import {
    AuthClientConfig,
    AuthConfig,
    authHttpInterceptorFn,
    provideAuth0
} from '@auth0/auth0-angular';
import {ROUTES} from './app/routes';
import {firstValueFrom} from "rxjs";
import {ConfigService} from "./app/core/services/config.service";

function authConfigInitializer(
    handler: HttpBackend,
    config: AuthClientConfig
) {
    return async () => {
        const httpClient = new HttpClient(handler);

        try {
            const loadedConfig = await firstValueFrom(httpClient.get('/assets/config-auth.json'));
            config.set(<AuthConfig>loadedConfig);
        } catch (error) {
            console.error('Error loading config:', error);
        }
    };
}

function apiConfigInitializer(configService: ConfigService) {
    return () => configService.loadConfigFromServer();
}

bootstrapApplication(AppComponent, {
    providers: [
        {
            provide: APP_INITIALIZER,
            useFactory: authConfigInitializer,
            deps: [HttpBackend, AuthClientConfig],
            multi: true,
        },
        ConfigService,
        {
            provide: APP_INITIALIZER,
            useFactory: apiConfigInitializer,
            deps: [ConfigService],
            multi: true,
        },
        provideAuth0(),
        provideHttpClient(
            withInterceptors([authHttpInterceptorFn])
        ),
        provideRouter(ROUTES,
            withPreloading(PreloadAllModules),
            //withDebugTracing(),
        ),
    ]
}).catch(err => console.error(err));

