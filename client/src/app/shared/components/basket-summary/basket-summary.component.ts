import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { Basket, BasketItem, BasketTotals } from '../../models/common/basket';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.scss']
})
export class BasketSummaryComponent implements OnInit {

  basket$: Observable<Basket | null>;
  basketTotals$: Observable<BasketTotals | null>;

  @Input() isBasket: boolean = true;

  @Output() decrement: EventEmitter<BasketItem> = new EventEmitter<BasketItem>();
  @Output() increment: EventEmitter<BasketItem> = new EventEmitter<BasketItem>();
  @Output() remove: EventEmitter<BasketItem> = new EventEmitter<BasketItem>();

  constructor(
    private basketService: BasketService
  ) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.basketTotals$ = this.basketService.basketTotal$;
  }

  decrementBasketItemQuantity(item: BasketItem) {
    this.decrement.emit(item);
  }

  incrementBasketItemQuantity(item: BasketItem) {
    this.increment.emit(item);
  }

  removeBasketItem(item: BasketItem) {
    this.remove.emit(item);
  }

  getBasketQuantityStyle() : string {
    return this.isBasket ? 'd-flex align-items-center gap-2' : 'ps-4'
  }

}
