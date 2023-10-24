import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { EMPTY_GUID } from 'src/app/shared/constants/constants';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {

  constructor(private http: HttpClient) {}

  get404Error() {
    return this.http.get(environment.apiUrl + "products/" + EMPTY_GUID).subscribe({
      next: () => {},
    });
  }
}
