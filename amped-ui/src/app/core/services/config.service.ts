import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class ConfigService {
    private config: any = {
        // Your default configuration settings go here
    };

    constructor() {}

    getConfig(): any {
        return this.config;
    }

    loadConfigFromServer(): Promise<void> {
        return fetch('/assets/config-api.json')
            .then((response) => response.json())
            .then((data) => {
                this.config = data;
            });
    }
}
