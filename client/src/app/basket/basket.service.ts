import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { Basket, BasketItem, BasketTotals, CustomerBasket } from '../shared/models/common/basket';
import { environment } from 'src/environments/environment';
import { Product } from '../shared/models/common/product';
import { DeliveryMethod } from '../shared/models/common/delivery-method';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  private basketSource = new BehaviorSubject<Basket | null>(null);
  basket$ = this.basketSource.asObservable();

  private basketTotalSource = new BehaviorSubject<BasketTotals | null>(null);
  basketTotal$ = this.basketTotalSource.asObservable();

  private shipping: number = 0;

  constructor(
    private http: HttpClient
  ) { }

  getBasket(id: string) {
    return this.http.get<Basket>(environment.apiUrl + 'basket?basketId=' + id)
      .pipe(
        map((basket: Basket) => {
          if(basket.basketItems.length > 0) {
            this.basketSource.next(basket);
            this.calculateBaksetTotals();
          } else {
            this.clearBasket();
          }
        })
      );
  }

  setBasket(basket: Basket) {
    return this.http.post<Basket>(environment.apiUrl + 'basket', basket).subscribe({
      next: (basket: Basket) => {
        this.basketSource.next(basket)
        this.calculateBaksetTotals();
      },
      error: (err: HttpErrorResponse) => console.log(err.message)
    });
  }

  clearBasket(): void {
    this.basketSource.next(null);
    this.basketTotalSource.next(null);
    localStorage.removeItem('basket_id');
  }
  
  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  setShippingPrice(deliveryMethod: DeliveryMethod) {
    this.shipping = deliveryMethod.price;
    this.calculateBaksetTotals();
  }

  addItemToBasket(item: Product, quantity = 1) {
    const itemToAdd: BasketItem = this.mapProductItemToBasketItem(item, quantity);
    // If the basket exists take it otherwise create a new one
    const basket : Basket = this.getCurrentBasketValue() ?? this.createBasket();

    // If basketItem already exists, increase the quantity otherwise add the item to the list of items
    basket.basketItems = this.addOrUpdateItem(basket.basketItems, itemToAdd, quantity)

    this.setBasket(basket);
  }

  incrementBasketItemQuantity(item: BasketItem) {
    let basket: Basket = this.getCurrentBasketValue() as Basket;
    let foundItemIndex = basket?.basketItems.findIndex(x => x.id === item.id);
    basket.basketItems[foundItemIndex].quantity++;
    this.setBasket(basket);
  }

  decrementBasketItemQuantity(item: BasketItem) {
    let basket: Basket = this.getCurrentBasketValue() as Basket;
    let foundItemIndex = basket?.basketItems.findIndex(x => x.id === item.id);
    let basketItem = basket.basketItems[foundItemIndex];
    if(basketItem.quantity > 1) {
      basketItem.quantity--;
      this.setBasket(basket);
    } else {
      this.removeItemFromBasket(item);
    }
  }

  removeItemFromBasket(item: BasketItem) {
    var basket: Basket = this.getCurrentBasketValue() as Basket;
    // Check to see if there is any item that match the items in the basket
    if(basket.basketItems.some(x => x.id === item.id)) {
      basket.basketItems = basket.basketItems.filter(x => x.id !== item.id);
      if(basket.basketItems.length > 0) {
        this.setBasket(basket);
      } 
      // If the removed item was the only item left in the basket
      else {
        this.deleteBasket(basket);
      }
    }
  }

  deleteBasket(basket: Basket) {
    return this.http.delete<boolean>(environment.apiUrl + "basket?basketId=" + basket.basketId).subscribe({
      next: () => {
        // reset the basket itself and the basket totals
        this.clearBasket();
      },
      error: (err: HttpErrorResponse) => console.error(err),
    });
  }

  private calculateBaksetTotals() {
    const basket = this.getCurrentBasketValue();

    var shipping = this.shipping;
    var subtotal = basket?.basketItems.reduce((result: number, item: BasketItem) => (item.quantity * item.price) + result, 0) as number;
    var total = subtotal + shipping;

    this.basketTotalSource.next({
      shipping,
      subtotal,
      total
    });
  }

  private addOrUpdateItem(basketItems: BasketItem[], basketItemToAdd: BasketItem, quantity: number) : BasketItem[] {
    const index = basketItems.findIndex(i => i.id === basketItemToAdd.id);

    if(index === -1) {
      basketItemToAdd.quantity = quantity;
      basketItems.push(basketItemToAdd)
    } else {
      basketItems[index].quantity += quantity;
    }

    return basketItems;
  }

  private createBasket() : Basket {
    // This will create an empty basket with a random identifier
    const basket: CustomerBasket = new CustomerBasket();
    localStorage.setItem('basket_id', basket.basketId);
    return basket;
  }

  private mapProductItemToBasketItem(item: Product, quantity: number) : BasketItem {
    return {
      id: item.productId,
      brand: item.productBrandName,
      type: item.productTypeName,
      pictureUrl: item.pictureUrl,
      price: item.price,
      productName: item.name,
      quantity
    }
  }

}


