import { Component, OnInit } from '@angular/core';
import { OrderService } from './order.service';
import { Order } from '../shared/models/common/order';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {
  
  orders: Order[];

  constructor(
    private orderService: OrderService
  ) {}

  ngOnInit(): void {
    this.loadUserOrders();
  }

  loadUserOrders(): void {
    this.orderService.getOrders().subscribe({
      next: (orders: Order[]) => {
        this.orders = orders;
        this.orders.map((order: Order) => {
          order.total = order.subtotal + order.deliveryMethod.price
        });
      },
      error: (err : HttpErrorResponse) => console.log(err)
    })
  }

  formatOrderDate(orderDate: string) {
    const date = new Date(orderDate);
    return date.toLocaleDateString();
  }

}
