import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { DeliveryMethod } from 'src/app/shared/models/common/delivery-method';
import { CheckoutService } from '../../checkout.service';
import { HttpErrorResponse } from '@angular/common/http';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss']
})
export class CheckoutDeliveryComponent implements OnInit {

  @Input() checkoutForm: FormGroup;

  deliveryMethods: DeliveryMethod[];

  constructor(
    private checkoutService: CheckoutService,
    private basketService: BasketService,
  ) {}

  ngOnInit(): void {
    this.getDeliveryMethods();
  }

  getDeliveryMethods(): void {
    this.checkoutService.getDeliveryMethods().subscribe({
      next: ((dm: DeliveryMethod[]) => this.deliveryMethods = dm),
      error: (err: HttpErrorResponse) => console.log(err)
    })
  }

  setShippingPrice(dm: DeliveryMethod) {
    this.basketService.setShippingPrice(dm);
  }

}
