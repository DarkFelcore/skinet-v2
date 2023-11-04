import { Component, ElementRef, ViewChild } from '@angular/core';
import { ShopService } from './shop.service';
import { Product } from '../shared/models/common/product';
import { Pagination } from '../shared/models/common/pagination';
import { Brand } from '../shared/models/common/brand';
import { map } from 'rxjs';
import { Type } from '../shared/models/common/type';
import { HttpErrorResponse } from '@angular/common/http';
import { EMPTY_GUID } from '../shared/constants/constants';
import { ShopParams } from '../shared/models/shop-params';
import { PaginationService } from '../shared/services/pagination.service';
import { TextInputComponent } from '../shared/components/text-input/text-input.component';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent {
  products: Product[] | undefined;
  brands: Brand[];
  types: Type[];

  shopParams: ShopParams = new ShopParams();

  pageSize: number | undefined;
  pageIndex: number | undefined;
  totalCount: number | undefined;

  searchInputValue: string;

  @ViewChild('search') searchTerm : ElementRef;

  constructor(
    private shopService: ShopService,
    private paginationService: PaginationService
  ) {}

  ngOnInit(): void {
    this.getCurrentPage();
    this.getBrands();
    this.getTypes();
  }

  getCurrentPage() {
    this.paginationService.currentPage$.subscribe((page) => {
      this.shopParams.page = page;
      this.getProducts();
    });
  }

  getBrands() {
    this.shopService.getProductBrands()
    .pipe(map((response: Brand[]) => [
      {productBrandId: EMPTY_GUID, name: 'All'},
      ...response
    ]))
    .subscribe({
      next: (response: Brand[]) => this.brands = response,
      error: (err: HttpErrorResponse) => console.log(err)
    })
  }

  getTypes() {
    this.shopService.getProductTypes()
    .pipe(map((response: Type[]) => [
      {productTypeId: EMPTY_GUID, name: 'All'},
      ...response
    ]))
    .subscribe({
      next: (response: Type[]) => this.types = response,
      error: (err: HttpErrorResponse) => console.log(err)
    })
  }

  getProducts(): void {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: (response: Pagination<Product> | null) => {
        this.products = response?.data;
        this.pageIndex = response?.pageIndex;
        this.pageSize = response?.pageSize;
        this.totalCount = response?.count;
      },
      error: (err: HttpErrorResponse) => console.error(err)
    })
  }

  onBrandSelected(brandId: string) {
    this.shopParams.brandId = brandId;
    this.paginationService.setCurrentPage(1);
  }
  
  onTypeSelected(typeId: string) {
    this.shopParams.typeId = typeId;
    this.paginationService.setCurrentPage(1);
  }
  
  onSortSelected(sort: string) {  
    this.shopParams.sort = sort;
    this.paginationService.setCurrentPage(1);
  }
  
  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.paginationService.setCurrentPage(1);
  }
  
  onReset() {
    this.searchTerm.nativeElement.value = null;
    this.shopParams = new ShopParams();
    this.paginationService.setCurrentPage(1);
    this.shopService.setResetValue(true);
  }
}
