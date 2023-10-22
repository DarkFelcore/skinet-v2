import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { EMPTY_GUID } from 'src/app/shared/constants/constants';
import { Brand } from 'src/app/shared/models/common/brand';
import { SortOptions } from 'src/app/shared/models/sort-options';
import { Type } from 'src/app/shared/models/common/type';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss']
})
export class FilterComponent implements OnInit {
  brandIdSelected: string = EMPTY_GUID;
  typeIdSelected: string = EMPTY_GUID;

  sortOptions: SortOptions[] = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' }
  ];
  selectedSort: string = this.sortOptions[0].value;

  @Input() brands: Brand[];
  @Input() types: Type[];

  @Output() sortEvent = new EventEmitter<string>();
  @Output() brandEvent = new EventEmitter<string>();
  @Output() typeEvent = new EventEmitter<string>();

  constructor(
    private shopService: ShopService
  ) {}

  ngOnInit() {
    this.shopService.reset$.subscribe((resetValue: boolean) => {
      if(resetValue) {
        this.onBrandSelected(EMPTY_GUID)
        this.onTypeSelected(EMPTY_GUID)
        this.selectedSort = this.sortOptions[0].value;
      }
    })
  }

  onSortSelected = (event: any) => {
    const sort = (event.target as HTMLSelectElement).value;
    this.sortEvent.emit(sort);
  }
  
  onBrandSelected = (brandId: string) => {
    this.brandIdSelected = brandId;
    this.brandEvent.emit(brandId);
  }

  onTypeSelected = (typeId: string) => {
    this.typeIdSelected = typeId;
    this.typeEvent.emit(typeId);
  }

}
