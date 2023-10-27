import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { Product } from 'src/app/shared/models/common/product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {
  @Input() product: Product;

  constructor(
    private basketService: BasketService
  ) {}

  ngOnInit(): void { }

  addProductToBasket() {
    this.basketService.addItemToBasket(this.product)
  }

}
