import {AfterViewInit, Component, ElementRef, OnDestroy, ViewChild} from '@angular/core';
import {PageLayoutComponent} from 'src/app/shared/components/page-layout.component';
import {AmpedBookmarkComponent} from '@app/shared';
import {AuthService} from "@auth0/auth0-angular";
import {CommonModule} from '@angular/common';
import {BookmarkModel, BookmarkService, UrlInfoService} from '@app/core';
import {debounceTime, distinctUntilChanged, fromEvent, Subject, switchMap, takeUntil} from "rxjs";
import {map} from "rxjs/operators";

@Component({
    standalone: true,
    imports: [CommonModule, PageLayoutComponent, AmpedBookmarkComponent],
    selector: 'app-home',
    templateUrl: './home.component.html',
})
export class HomeComponent implements AfterViewInit, OnDestroy {
    @ViewChild('bookmark') searchInputRef!: ElementRef<HTMLInputElement>;
    bookmarks: BookmarkModel[] = [];
    isAuthenticated$ = this.authService.isAuthenticated$
    private debounceTime = 500;
    private destroy$ = new Subject<void>(); // To unsubscribe from the fromEvent observable

    constructor(private authService: AuthService, public bookmarkService: BookmarkService, public urlInfoService: UrlInfoService) {
    }

    ngOnInit(): void {
        this.bookmarkService.getFeed().subscribe((response) => {
            const {data, error} = response;

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
            const {data, error} = response;

            if (data) {
                console.log(data);
            }

            if (error) {
                console.log(JSON.stringify(error, null, 2));
            }
        });
    }

    ngAfterViewInit() {
        const bookmarkInput = this.searchInputRef.nativeElement;

        // Use fromEvent to listen to 'input' events on the bookmark input
        fromEvent(bookmarkInput, 'input').pipe(
            map((event: any) => event.target.value),
            debounceTime(this.debounceTime),
            distinctUntilChanged(),
            switchMap((query: string) => this.urlInfoService.getInfo(query)),
            takeUntil(this.destroy$) // Unsubscribe when the component is destroyed
        ).subscribe((response) => {
            const {data, error} = response;

            if (data) {
                console.log(data);
            }

            if (error) {
                console.log(JSON.stringify(error, null, 2));
            }
        });
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }
}
