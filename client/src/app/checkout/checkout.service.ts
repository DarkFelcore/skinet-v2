import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { DeliveryMethod } from '../shared/models/common/delivery-method';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Order } from '../shared/models/common/order';
import { CreateOrderRequest } from '../shared/models/create-order-request';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService implements OnInit {

  constructor(
    private http: HttpClient
  ) { }

  ngOnInit(): void {
    this.getDeliveryMethods();
  }

  getDeliveryMethods() : Observable<DeliveryMethod[]> {
    return this.http.get<DeliveryMethod[]>(environment.apiUrl + 'orders/deliverymethods');
  }
  
  createOrder(order: CreateOrderRequest) {
    return this.http.post<Order>(environment.apiUrl + 'orders', order);
  }
}
