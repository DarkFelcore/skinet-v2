import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/auth/auth.service';
import { BasketService } from 'src/app/basket/basket.service';
import { Basket } from 'src/app/shared/models/common/basket';
import { User } from 'src/app/shared/models/common/user';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  basket$: Observable<Basket | null>;
  currentUser$: Observable<User | null>;

  constructor(
    private basketService: BasketService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.currentUser$ = this.authService.currentUser$;
  }

  logout() {
    this.authService.logout();
  }

}
