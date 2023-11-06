import { CdkStepper } from '@angular/cdk/stepper';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { Basket } from 'src/app/shared/models/common/basket';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrls: ['./checkout-review.component.scss']
})
export class CheckoutReviewComponent implements OnInit {

  @Input() appStepper: CdkStepper;

  basket$: Observable<Basket | null>;

  constructor(
    private basketService: BasketService,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }

  createPaymentIntent() {
    return this.basketService.createPaymentIntent().subscribe({
      next: () => {
        this.appStepper.next();
      },
      error: (err: HttpErrorResponse) => {
        console.log(err)
        this.toastrService.error(err.message);
      }
    });
  }

}
