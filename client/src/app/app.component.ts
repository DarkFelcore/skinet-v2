import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Pagination } from './shared/models/common/pagination';
import { Product } from './shared/models/common/product';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  constructor(
    private backendService: BasketService
  ) { }

  ngOnInit(): void { 
    const basketId : string | null = localStorage.getItem('basket_id');
    if(basketId) {
      this.backendService.getBasket(basketId).subscribe();
    }
  }
}
