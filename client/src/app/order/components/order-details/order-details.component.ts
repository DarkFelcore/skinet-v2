import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Order } from 'src/app/shared/models/common/order';
import { BreadcrumbService } from 'xng-breadcrumb';
import { OrderService } from '../../order.service';
import { HttpErrorResponse } from '@angular/common/http';
import { BasketService } from 'src/app/basket/basket.service';
import { Basket } from 'src/app/shared/models/common/basket';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.scss']
})
export class OrderDetailsComponent implements OnInit {

  order: Order;

  basket$: Observable<Basket | null>;

  constructor(
    private activatedRoute: ActivatedRoute,
    private breadcrumbService: BreadcrumbService,
    private orderService: OrderService,
    private basketService: BasketService,
    private router: Router
  ) {
    this.breadcrumbService.set('@orderDetails', 'Loading...');
  }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.getOrder();
  }

  getOrder() {
    this.orderService.getOrder(this.activatedRoute.snapshot.paramMap.get('orderId') as string).subscribe({
      next: (order: Order) => {
        this.order = order;
        this.breadcrumbService.set('@orderDetails', `# ${this.order.orderId} - ${this.order.status}`);
      },
      error: (err: HttpErrorResponse) => this.router.navigateByUrl('/orders')
    });


  }

}
