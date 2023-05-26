import { Component } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-page-loader',
  template: `
    <div class="loader">
      <img [src]="loadingImg" alt="Loading..." />
    </div>
  `,
})
export class PageLoaderComponent {
  loadingImg = '/assets/loader.svg';
}
