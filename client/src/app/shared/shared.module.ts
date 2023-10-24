import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from './components/button/button.component';
import { TextInputComponent } from './components/text-input/text-input.component';
import { IconButtonComponent } from './components/icon-button/icon-button.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { PaginationHeaderComponent } from './components/pagination-header/pagination-header.component';
import { FormsModule } from '@angular/forms';
import { CarouselModule } from 'ngx-bootstrap/carousel';

@NgModule({
  declarations: [
    ButtonComponent,
    TextInputComponent,
    IconButtonComponent,
    PaginationComponent,
    PaginationHeaderComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    CarouselModule.forRoot(),
  ],
  exports: [
    ButtonComponent,
    TextInputComponent,
    IconButtonComponent,
    PaginationComponent,
    PaginationHeaderComponent,
    CarouselModule
  ]
})
export class SharedModule { }
