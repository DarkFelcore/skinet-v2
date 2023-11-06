import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Brand } from 'src/app/shared/models/common/brand';
import { SortOptions } from 'src/app/shared/models/sort-options';
import { Type } from 'src/app/shared/models/common/type';
import { ShopParams } from 'src/app/shared/models/shop-params';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss']
})
export class FilterComponent implements OnInit {
  sortOptions: SortOptions[] = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' }
  ];

  @Input() brands: Brand[];
  @Input() types: Type[];

  @Input() shopParams: ShopParams;

  @Output() sortEvent = new EventEmitter<string>();
  @Output() brandEvent = new EventEmitter<string>();
  @Output() typeEvent = new EventEmitter<string>();

  constructor(
  ) {}

  ngOnInit() { }

  onSortSelected = (event: any) => {
    const sort = (event.target as HTMLSelectElement).value;
    this.sortEvent.emit(sort);
  }
  
  onBrandSelected = (brandId: string) => {
    this.brandEvent.emit(brandId);
  }

  onTypeSelected = (typeId: string) => {
    this.typeEvent.emit(typeId);
  }

}
