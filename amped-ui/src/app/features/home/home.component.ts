import { Component } from '@angular/core';
import { PageLayoutComponent } from 'src/app/shared/components/page-layout.component';
import { AmpedBookmarkComponent } from '@app/shared';
import { AuthService } from "@auth0/auth0-angular";
import { CommonModule } from '@angular/common';
import { BookmarkModel, BookmarkService } from '@app/core';

@Component({
  standalone: true,
  imports: [CommonModule, PageLayoutComponent, AmpedBookmarkComponent],
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  bookmarks: BookmarkModel[] = [];

  isAuthenticated$ = this.authService.isAuthenticated$
  constructor(private authService: AuthService, public bookmarkService: BookmarkService) {}

  ngOnInit(): void {
    this.bookmarkService.getFeed().subscribe((response) => {
      const { data, error } = response;

      if (data) {
          Object.values(data).forEach(val => {
          this.bookmarks.push(val);
         })
      }

      if (error) {
        console.log(JSON.stringify(error, null, 2));
      }
    });
  }

  addBookmark(uri: string) {
    this.bookmarkService.addBookmark(uri).subscribe((response) => {
      const { data, error } = response;

      if (data) {
          console.log(data);
      }

      if (error) {
        console.log(JSON.stringify(error, null, 2));
      }
    });
  }

}
