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

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent {
  @ViewChild('search') searchTerm : ElementRef;

  products: Product[];
  brands: Brand[];
  types: Type[];

  shopParams: ShopParams;

  totalProductCount: number;

  constructor(
    private shopService: ShopService,
  ) {
    this.shopParams = this.shopService.getShopParams();
  }

  ngOnInit(): void {
    this.getProducts(true);
    this.getBrands();
    this.getTypes();
  }

  getBrands(): void {
    this.shopService.getProductBrands().subscribe({
      next: (response) => this.brands = [{name: "All", productBrandId: EMPTY_GUID}, ...response],
      error: (e: HttpErrorResponse) => console.log(e)
    })
  }

  getTypes(): void {
    this.shopService.getProductTypes().subscribe({
      next: (response) => this.types = [{ name: "All", productTypeId: EMPTY_GUID}, ...response],
      error: (e: HttpErrorResponse) => console.log(e)
    })
  }

  getProducts(useCache = false): void {
    this.shopService.getProducts(useCache).subscribe({
      next: (response: Pagination<Product> | null) => {
        this.products = response?.data as Product[];
        this.totalProductCount = response?.count as number;
      },
      error: (err: HttpErrorResponse) => console.error(err)
    })
  }

  onBrandSelected(brandId: string) {
    const params: ShopParams = this.shopService.getShopParams();
    params.brandId = brandId;
    params.pageNumber = 1;
    this.shopService.setShopParams(params);
    this.getProducts(true);
  }
  
  onTypeSelected(typeId: string) {
    const params: ShopParams = this.shopService.getShopParams();
    params.typeId = typeId;
    params.pageNumber = 1;
    this.shopService.setShopParams(params);
    this.getProducts(true);
  }
  
  onSortSelected(sort: string) {  
    const params: ShopParams = this.shopService.getShopParams();
    params.sort = sort;
    params.pageNumber = 1;
    this.shopService.setShopParams(params);
    this.getProducts(true);
  }

  onPageChanged(page: number) {
    const params = this.shopService.getShopParams();
    // The if statement is needed because when a filter is selected the getProduct method is called twice (duplicate api request for the same data)
    // in order to resolve this, there should be a check if this method is called only for changing the page, in this case the products can be requested form the api
    if(params.pageNumber !== page) {
      params.pageNumber = page;
      this.shopService.setShopParams(params);
      this.getProducts(true);
    }
  }
  
  onSearch() {
    const params: ShopParams = this.shopService.getShopParams();
    params.search = this.searchTerm.nativeElement.value;
    params.pageNumber = 1;
    this.shopService.setShopParams(params);
    this.getProducts(true);
  }
  
  onReset() {
    this.searchTerm.nativeElement.value = "";
    this.shopParams = new ShopParams();
    this.shopService.setShopParams(this.shopParams);
    //this.shopService.setResetValue(true);
    this.getProducts();
  }
}
