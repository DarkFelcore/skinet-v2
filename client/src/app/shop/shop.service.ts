import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/common/pagination';
import { Product } from '../shared/models/common/product';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { Brand } from '../shared/models/common/brand';
import { Type } from '../shared/models/common/type';
import { EMPTY_GUID } from '../shared/constants/constants';
import { ShopParams } from '../shared/models/shop-params';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  private baseUrl: string = 'https://localhost:7075/api/';

  private resetSubject = new BehaviorSubject<boolean>(false);
  reset$ = this.resetSubject.asObservable();

  constructor(
    private http: HttpClient
  ) { }

  getProducts(shopParams: ShopParams) : Observable<Pagination<Product> | null> {
    const params = this.getProductParams(shopParams);

    return this.http.get<Pagination<Product>>(this.baseUrl + 'products', {
      observe: 'response',
      params
    }).pipe(map(response => {
      return response.body;
    }));
  }

  getProductParams(shopParams: ShopParams): HttpParams {
    let params = new HttpParams();

    if(shopParams.sort) {
      params = params.append('sort', shopParams.sort);
    }

    if(shopParams.brandId && shopParams.brandId !== EMPTY_GUID) {
      params = params.append('brandId', shopParams.brandId);
    }

    if(shopParams.typeId && shopParams.typeId !== EMPTY_GUID) {
      params = params.append('typeId', shopParams.typeId);
    }

    if(shopParams.page) {
      params = params.append('pageIndex', shopParams.page);
    }

    if(shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    return params;
  }

  getProductById(productId: string): Observable<Product> {
    return this.http.get<Product>(this.baseUrl + 'products/' + productId);
  }

  getProductBrands(): Observable<Brand[]> {
    return this.http.get<Brand[]>(this.baseUrl + 'products/brands');
  }

  getProductTypes(): Observable<Type[]> {
    return this.http.get<Type[]>(this.baseUrl + 'products/types');
  }

  setResetValue(value: boolean) {
    this.resetSubject.next(value)
  }

}
