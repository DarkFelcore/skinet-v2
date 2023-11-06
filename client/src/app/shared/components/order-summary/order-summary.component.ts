import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketTotals } from '../../models/common/basket';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-order-summary',
  templateUrl: './order-summary.component.html',
  styleUrls: ['./order-summary.component.scss']
})
export class OrderSummaryComponent implements OnInit {
  
  @Input() subtotal: string;
  @Input() shippingPrice: string;
  @Input() total: string;

  ngOnInit(): void {
    this.total = String(this.getTotalPrice());
  }

  getTotalPrice(): number {
    return Number(this.shippingPrice)  + Number(this.subtotal);
  }

}
