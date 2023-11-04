import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Order } from 'src/app/shared/models/common/order';
import { CheckoutService } from '../../checkout.service';
import { BasketService } from 'src/app/basket/basket.service';
import { AuthService } from 'src/app/auth/auth.service';
import { User } from 'src/app/shared/models/common/user';
import { HttpErrorResponse } from '@angular/common/http';
import { CreateOrderRequest } from 'src/app/shared/models/create-order-request';
import { Basket } from 'src/app/shared/models/common/basket';
import { ToastrService } from 'ngx-toastr';
import { NavigationExtras, Router } from '@angular/router';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements OnInit {
  
  @Input() checkoutForm: FormGroup;
  @Input() currentUser: User;

  constructor(
    private checkoutService: CheckoutService,
    private basketService: BasketService,
    private toastrService: ToastrService,
    private router: Router
  ) {}
  
  ngOnInit(): void {

  }

  submitOrder() {
    const basket = this.basketService.getCurrentBasketValue();
    this.checkoutService.createOrder(this.mapCheckoutFormToOrder(basket as Basket)).subscribe({
      next: (order: Order) => {
        this.toastrService.success("Order created successfully")
        this.basketService.deleteBasket(basket as Basket);
        
        const navigationExtras: NavigationExtras = { state: order }
        this.router.navigate(['/checkout/success'], navigationExtras);
      },
      error: (err: HttpErrorResponse) => console.log(err)
    });
  }

  mapCheckoutFormToOrder(basket: Basket) : CreateOrderRequest {
    return {
      basketId: basket.basketId,
      deliveryMethodId: this.checkoutForm.get('deliveryForm')?.value['deliveryMethod'],
      address: {
        firstName: this.currentUser.firstName,
        lastName: this.currentUser.lastName,
        ...this.checkoutForm.get('addressForm')?.value
      }
    }
  }

}
