import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, of } from 'rxjs';
import { Order } from '../shared/models/common/order';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  orders: Order[] = [];

  constructor(
    private http: HttpClient
  ) { }

  getOrder(id: string): Observable<Order> {

    let order = this.orders.find(o => o.orderId === id);

    if(order) {
      return of(order);
    }

    return this.http.get<Order>(environment.apiUrl + 'orders/' + id);
  }

  getOrders(): Observable<Order[]> {

    if(this.orders.length > 0) {
      return of(this.orders);
    }

    return this.http.get<Order[]>(environment.apiUrl + 'orders').pipe(
      map((orders: Order[]) => {
        this.orders = orders;
        return orders;
      })
    );
  }

}
