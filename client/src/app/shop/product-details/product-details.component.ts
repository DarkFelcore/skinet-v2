import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { Product } from 'src/app/shared/models/common/product';
import { HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: Product;

  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private breadcrumbService: BreadcrumbService
  ) {
    this.breadcrumbService.set('@productDetails', 'Loading...')
  }

  ngOnInit(): void {
    this.getProduct();
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
