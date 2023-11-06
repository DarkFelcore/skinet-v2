import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { AuthService } from './auth/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  constructor(
    private backendService: BasketService,
    private authService: AuthService
  ) { }

  ngOnInit(): void { 
    this.loadBasket();
    this.loadCurrentUser();
  }
  
  loadBasket() {
    const basketId : string | null = localStorage.getItem('basket_id');
    if(basketId) {
      this.backendService.getBasket(basketId).subscribe();
    }
  }

  loadCurrentUser() {
    const token: string = localStorage.getItem('token') as string;
    this.authService.loadCurrentUser(token).subscribe({
      next: () => {},
      error: () => this.authService.clearUser()
    });
  }
}
