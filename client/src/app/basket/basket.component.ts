import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket.service';
import { Basket, BasketItem } from '../shared/models/common/basket';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {

  basket$: Observable<Basket | null>;

  constructor(
    private basketService: BasketService
  ) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }

  incrementBasketItemQuantity(item: BasketItem) {
    this.basketService.incrementBasketItemQuantity(item);
  }

  decrementBasketItemQuantity(item: BasketItem) {
    this.basketService.decrementBasketItemQuantity(item);
  }

  removeBasketItem(item: BasketItem) {
    this.basketService.removeItemFromBasket(item);
  }

}
