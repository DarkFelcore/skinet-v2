import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/common/pagination';
import { Product } from '../shared/models/common/product';
import { BehaviorSubject, Observable, map, of } from 'rxjs';
import { Brand } from '../shared/models/common/brand';
import { Type } from '../shared/models/common/type';
import { EMPTY_GUID } from '../shared/constants/constants';
import { ShopParams } from '../shared/models/shop-params';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  brands: Brand[] | undefined = [];
  types: Type[] | undefined = [];

  pagination: Pagination<Product> = new Pagination<Product>();
  shopParams: ShopParams = new ShopParams();

  productCache = new Map();

  constructor(
    private http: HttpClient
  ) { }

  getProducts(useCache: boolean) : Observable<Pagination<Product> | null> {
    if(useCache === false) {
      this.productCache = new Map();
    }
    
    // If we want to cache and there are items in the map already
    if(this.productCache.size > 0 && useCache === true) {
      // Check to see if already available in cache by checking the key
      // If the data could not be find in the cache, the rest of the code will be executed and the api request will be made
      if(this.productCache.has(Object.values(this.shopParams).join('-'))) {
        this.pagination.data = this.productCache.get(Object.values(this.shopParams).join('-'))
        return of(this.pagination);
      }
    }

    // Make call to backend
    const params = this.createShopParams();

    return this.http.get<Pagination<Product>>(environment.apiUrl + 'products', {
      observe: 'response',
      params
    }).pipe(map(response => {
      // Set the obtained data from the api in cache map
      this.productCache.set(Object.values(this.shopParams).join('-'), response.body?.data)

      this.pagination = response.body as Pagination<Product>;

      return this.pagination;
    }));
  }

  getProductById(productId: string): Observable<Product> {
    let product: Product | undefined;
  
    // Check in cache to see if the product is available with the given id
    this.productCache.forEach((products: Product[]) => {
      const foundProduct = products.find(p => p.productId === productId);
      if (foundProduct) {
        product = foundProduct;
      }
    });
  
    // If the product is available in cache, return the cached product
    if (product) {
      return of(product);
    }
  
    // If the product is not in the cache, make an HTTP request to fetch it
    return this.http.get<Product>(environment.apiUrl + 'products/' + productId);
  }
  

  getProductBrands(): Observable<Brand[]> {
    // Prevent duplicate requests to the backend
    if(this.brands?.length as number > 0) {
      return of(this.brands as Brand[]);
    }

    return this.http.get<Brand[]>(environment.apiUrl + 'products/brands').pipe(
      map((brands: Brand[]) => {
        this.brands = brands;
        return brands;
      })
    );
  }

  getProductTypes(): Observable<Type[]> {

    // Prevent duplicate requests to the backend
    if(this.types?.length as number > 0) {
      return of(this.types as Type[]);
    }

    return this.http.get<Type[]>(environment.apiUrl + 'products/types').pipe(
      map((types: Type[]) => {
        this.types = types;
        return types;
      })
    );
  }

  getShopParams(): ShopParams {
    return this.shopParams;
  }

  setShopParams(params: ShopParams) {
    this.shopParams = params;
  }

  private createShopParams(): HttpParams {
    let params = new HttpParams();

    if(this.shopParams.sort) {
      params = params.append('sort', this.shopParams.sort);
    }

    if(this.shopParams.brandId && this.shopParams.brandId !== EMPTY_GUID) {
      params = params.append('brandId', this.shopParams.brandId);
    }

    if(this.shopParams.typeId && this.shopParams.typeId !== EMPTY_GUID) {
      params = params.append('typeId', this.shopParams.typeId);
    }

    if(this.shopParams.pageNumber) {
      params = params.append('pageIndex', this.shopParams.pageNumber);
    }

    if(this.shopParams.search) {
      params = params.append('search', this.shopParams.search);
    }

    return params;
  }

}
