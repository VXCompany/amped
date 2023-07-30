import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthService } from "@auth0/auth0-angular";
import { CommonModule } from '@angular/common';
import { PageLoaderComponent } from './shared';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,
  imports: [CommonModule, RouterOutlet, PageLoaderComponent]
})
export class AppComponent {
  isAuth0Loading$ = this.authService.isLoading$;
  constructor(public authService: AuthService) {}
}
