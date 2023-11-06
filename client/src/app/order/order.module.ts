import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderComponent } from './order.component';
import { OrderRoutingModule } from './order-routing.module';
import { OrderDetailsComponent } from './components/order-details/order-details.component';
import { SharedModule } from '../shared/shared.module';
import { OrderBasketSummaryComponent } from './components/order-basket-summary/order-basket-summary.component';



@NgModule({
  declarations: [
    OrderComponent,
    OrderDetailsComponent,
    OrderBasketSummaryComponent
  ],
  imports: [
    CommonModule,
    OrderRoutingModule,
    SharedModule
  ]
})
export class OrderModule { }
