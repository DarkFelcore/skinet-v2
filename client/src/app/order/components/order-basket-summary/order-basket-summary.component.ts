import { Component, Input, OnInit } from '@angular/core';
import { OrderItem } from 'src/app/shared/models/common/order-item';

@Component({
  selector: 'app-order-basket-summary',
  templateUrl: './order-basket-summary.component.html',
  styleUrls: ['./order-basket-summary.component.scss']
})
export class OrderBasketSummaryComponent implements OnInit {
  
  @Input() orderItems: OrderItem[] = [];

  constructor(

  ) {}
  
  ngOnInit(): void {

  }

}
