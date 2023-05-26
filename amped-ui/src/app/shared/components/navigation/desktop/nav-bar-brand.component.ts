import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  imports: [RouterModule],
  selector: 'app-nav-bar-brand',
  template: `
    <div class="nav-bar__brand">
      <a routerLink="/">
        <img
          class="nav-bar__logo"
          src="/assets/amped.svg"
          alt="Auth0 shield logo"
          height="32"
        />
      </a>
    </div>
  `,
})
export class NavBarBrandComponent {}
