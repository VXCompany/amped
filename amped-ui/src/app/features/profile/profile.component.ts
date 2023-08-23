import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '@auth0/auth0-angular';
import { PageLayoutComponent } from 'src/app/shared/components/page-layout.component';
import { CodeSnippetComponent } from 'src/app/shared/components/code-snippet.component';
import { AmpedProfileModel, ProfileService } from '@app/core';
import { FormsModule } from '@angular/forms';
import { map } from 'rxjs/operators';

@Component({
  standalone: true,
  imports: [CommonModule, PageLayoutComponent, CodeSnippetComponent, FormsModule],
  selector: 'app-profile',
  templateUrl: './profile.component.html',
})
export class ProfileComponent {

  isNewProfile: Boolean = false;

  title = 'Decoded ID Token';

  user$ = this.authService.user$;
  code$ = this.user$.pipe(map((user) => JSON.stringify(user, null, 2)));

  profile: AmpedProfileModel = {
    nickname: '',
    userid: '',
    bio: ''
  };

  constructor(private authService: AuthService, public profileService: ProfileService) {}

  ngOnInit(): void {
    this.profileService.getProfile().subscribe((response) => {
      const { data, error } = response;

      if (data) {
        this.profile = data as AmpedProfileModel;
      }

      if (error) {
        console.log(JSON.stringify(error, null, 2));
        if (error.status == "404") {
          this.isNewProfile = true;
        }
      }
    });
  }

  createProfile(nickname: string, bio:string) {
    this.profileService.createProfile(nickname, bio).subscribe((response) => {
      const { data, error } = response;

      if (data) {
          console.log(data);
      }

      if (error) {
        console.log(JSON.stringify(error, null, 2));
      }
    });
  }

    updateProfile(nickname: string, bio:string) {
    this.profileService.updateProfile(nickname, bio).subscribe((response) => {
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
