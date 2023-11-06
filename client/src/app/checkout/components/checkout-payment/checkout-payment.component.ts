import { AfterViewInit, Component, ElementRef, Input, OnDestroy, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Order } from 'src/app/shared/models/common/order';
import { CheckoutService } from '../../checkout.service';
import { BasketService } from 'src/app/basket/basket.service';
import { User } from 'src/app/shared/models/common/user';
import { HttpErrorResponse } from '@angular/common/http';
import { CreateOrderRequest } from 'src/app/shared/models/create-order-request';
import { Basket } from 'src/app/shared/models/common/basket';
import { ToastrService } from 'ngx-toastr';
import { NavigationExtras, Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';

declare var Stripe: any;

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements AfterViewInit, OnDestroy {

  stripe: any;
  
  @Input() checkoutForm: FormGroup;
  @Input() currentUser: User;

  @ViewChild('cardNumber', {static: true}) cardNumberElement: ElementRef;
  @ViewChild('cardExpiry', {static: true}) cardExpiryElement: ElementRef;
  @ViewChild('cardCvc', {static: true}) cardCvcElement: ElementRef;

  cardNumber: any;
  cardExpiry: any;
  cardCvc: any;
  cardErrors: any;

  cardNumberValid: boolean = false;
  cardExpiryValid: boolean = false;
  cardCvcValid: boolean = false;

  cardHandler = this.onChange.bind(this);

  loading = false;

  constructor(
    private checkoutService: CheckoutService,
    private basketService: BasketService,
    private toastrService: ToastrService,
    private router: Router
  ) {}

  ngAfterViewInit(): void {
    this.stripe = Stripe('pk_test_51O8mYLG4vY1rxPgS6WCvUs9t2pJKIJ6zpGBqM8kdhFqFOtFRJakcWGMAiTbQ8ruaxDS7Ejwwi2Lv77J09yeGeiNS00BHqKHVcV');
    var elements = this.stripe.elements();

    this.cardNumber = elements.create('cardNumber');
    this.cardNumber.mount(this.cardNumberElement.nativeElement);
    this.cardNumber.addEventListener('change', this.cardHandler);
    
    this.cardExpiry = elements.create('cardExpiry');
    this.cardExpiry.mount(this.cardExpiryElement.nativeElement);
    this.cardExpiry.addEventListener('change', this.cardHandler);
    
    this.cardCvc = elements.create('cardCvc');
    this.cardCvc.mount(this.cardCvcElement.nativeElement);
    this.cardCvc.addEventListener('change', this.cardHandler);
  }

  ngOnDestroy() {
    this.cardNumber.destroy();
    this.cardExpiry.destroy();
    this.cardCvc.destroy();
  }

  onChange(event: any) {
    if(event.error) {
      this.cardErrors = event.error.message;
    } else {
      this.cardErrors = null;
    }
    switch (event.elementType) {
      case 'cardNumber':
        this.cardNumberValid = event.complete;
        break;
      case 'cardExpiry':
        this.cardExpiryValid = event.complete;
        break;
      case 'cardCvc':
        this.cardCvcValid = event.complete;
        break;
      default:
        break;
    }
  }

  async submitOrder() {
    this.loading = true;
    const basket = this.basketService.getCurrentBasketValue();
    try {
      if(basket) {
        const createdOrder = await this.createOrder(basket as Basket);
        const paymentResult = await this.confirmPaymentWithStripe(basket);
  
        if(paymentResult.paymentIntent) {
          this.basketService.deleteBasket(basket as Basket);
          const navigationExtras: NavigationExtras = { state: createdOrder }
          this.router.navigate(['/checkout/success'], navigationExtras);
        } else {
          this.toastrService.error(paymentResult.error.message);
        }
      }
      this.loading = false;
    } catch (error) {
      console.log(error);
      this.loading = false;
    }
  }

  private async confirmPaymentWithStripe(basket: Basket) {
    return this.stripe.confirmCardPayment(basket.clientSecret, {
      payment_method: {
        card: this.cardNumber,
        billing_details: {
          name: this.checkoutForm.get('paymentForm')?.get('nameOnCard')?.value,
        }
      }
    })
  }

  private async createOrder(basket: Basket) {
    const orderToCreate = this.mapCheckoutFormToOrder(basket);
    return lastValueFrom(this.checkoutService.createOrder(orderToCreate))
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
