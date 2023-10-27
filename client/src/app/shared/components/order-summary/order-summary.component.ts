import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketTotals } from '../../models/common/basket';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-order-summary',
  templateUrl: './order-summary.component.html',
  styleUrls: ['./order-summary.component.scss']
})
export class OrderSummaryComponent implements OnInit {
  
  basketTotals$: Observable<BasketTotals | null>;
  
  constructor(
    private basketService: BasketService
  ) {}

  ngOnInit(): void {
    this.basketTotals$ = this.basketService.basketTotal$;
  }

}
