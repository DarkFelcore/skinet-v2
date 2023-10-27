import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { Product } from 'src/app/shared/models/common/product';
import { HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: Product;
  quantity: number = 1;

  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private breadcrumbService: BreadcrumbService,
    private basketService: BasketService
  ) {
    this.breadcrumbService.set('@productDetails', 'Loading...')
  }

  ngOnInit(): void {
    this.getProduct();
  }

  addProductToBasket() {
    this.basketService.addItemToBasket(this.product, this.quantity);
  }

  incrementProductQuantity() {
    this.quantity++;
  }

  decrementProductQuantity() {
    if(this.quantity > 1) {
      this.quantity--;
    }
  }

  getProduct() {
    this.shopService.getProductById(String(this.activatedRoute.snapshot.paramMap.get('productId'))).subscribe({
      next: (response: Product) => {
        this.product = response;
        this.breadcrumbService.set('@productDetails', this.product.name ?? '')
      },
      error: (err: HttpErrorResponse) => console.log(err)
    })
  }
}
