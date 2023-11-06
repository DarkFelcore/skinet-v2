import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from '../shared/models/common/user';
import { AuthService } from '../auth/auth.service';
import { Address } from '../shared/models/common/address';
import { BasketService } from '../basket/basket.service';
import { Basket, BasketTotals } from '../shared/models/common/basket';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {
  currentUser: User;
  checkoutForm: FormGroup;

  basket$: Observable<Basket | null>;
  basketTotals$: Observable<BasketTotals | null>;

  constructor(
    private fb : FormBuilder,
    private authService: AuthService,
    private basketService: BasketService
  ) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.basketTotals$ = this.basketService.basketTotal$;
    this.loadCurrentUser();
  }

  createCheckoutForm() {
    this.checkoutForm = this.fb.group({
      addressForm: this.fb.group({
        firstName: [this.currentUser.firstName, Validators.required],
        lastName: [this.currentUser.lastName, Validators.required],
        street: ['', Validators.required],
        postalCode: ['', Validators.required],
        provice: ['', Validators.required],
        city: ['', Validators.required],
        country: ['', Validators.required],
      }),
      deliveryForm: this.fb.group({
        deliveryMethod: [null, Validators.required]
      }),
      paymentForm: this.fb.group({
        nameOnCard: [null, Validators.required]
      })
    })
  }

  loadCurrentUser() {
    const token: string = localStorage.getItem('token') as string;
    this.authService.loadCurrentUser(token).subscribe({
      next: (user: User | null) => {
        if(user) {
          this.currentUser = user;
          this.createCheckoutForm()
          this.loadCurrentUserAddress();
          this.getDeliveryMethodValue();
        }
      },
    });
  }

  loadCurrentUserAddress() {
    this.authService.getUserAddress().subscribe({
      next: (address: Address) => {
        if(address) {
          this.checkoutForm.get('addressForm')?.patchValue(address);
        }
      },
    });
  }

  getDeliveryMethodValue() {
    const basket = this.basketService.getCurrentBasketValue();
    if(basket?.deliveryMethodId !== null) {
      this.checkoutForm.get('deliveryForm')?.get('deliveryMethod')?.patchValue(basket?.deliveryMethodId);
    }
  }
}
