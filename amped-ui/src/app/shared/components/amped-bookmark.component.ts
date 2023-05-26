import { Component, Input } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-amped-bookmark',
  template: `
    <a
      class="amped-bookmark"
      target="_blank"
      rel="noopener noreferrer"
      [href]="resourceUrl"
    >
      <h3 class="amped-bookmark__headline">
        <img
          class="amped-bookmark__icon"
          [src]="icon"
          alt="external link icon"
        />
        {{ title }}
      </h3>
      <p class="amped-bookmark__description">{{ description }}</p>
    </a>
  `,
})
export class AmpedBookmarkComponent {
  @Input() title: string | undefined;
  @Input() description: string | undefined;
  @Input() resourceUrl: string | undefined;
  @Input() read: boolean | undefined;

  icon = "";
  icons = ["/assets/bm1.svg","/assets/bm2.svg","/assets/bm3.svg","/assets/bm4.svg"]
 
  ngOnInit() {
    this.icon = this.icons[Math.floor(Math.random() * 4)];
  }
  
}
