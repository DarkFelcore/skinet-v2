import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from './components/button/button.component';
import { TextInputComponent } from './components/text-input/text-input.component';
import { IconButtonComponent } from './components/icon-button/icon-button.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { PaginationHeaderComponent } from './components/pagination-header/pagination-header.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { OrderSummaryComponent } from './components/order-summary/order-summary.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import {CdkStepperModule} from '@angular/cdk/stepper';
import { StepperComponent } from './components/stepper/stepper.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { BasketSummaryComponent } from './components/basket-summary/basket-summary.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    ButtonComponent,
    TextInputComponent,
    IconButtonComponent,
    PaginationComponent,
    PaginationHeaderComponent,
    OrderSummaryComponent,
    StepperComponent,
    BasketSummaryComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CdkStepperModule,
    MatTooltipModule,
    RouterModule,
    CarouselModule.forRoot(),
    BsDropdownModule.forRoot(),
  ],
  exports: [
    ButtonComponent,
    TextInputComponent,
    IconButtonComponent,
    PaginationComponent,
    PaginationHeaderComponent,
    OrderSummaryComponent,
    StepperComponent,
    BasketSummaryComponent,
    CarouselModule,
    ReactiveFormsModule,
    BsDropdownModule,
    CdkStepperModule,
    MatTooltipModule
  ]
})
export class SharedModule { }
