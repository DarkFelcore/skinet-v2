import { v4 as uuidv4 } from 'uuid';

export interface Basket {
    basketId: string;
    basketItems: BasketItem[];
    clientSecret?: string;
    paymentIntentId?: string;
    deliveryMethodId?: string;
    shippingPrice?: number;
}

export interface BasketItem {
    id: string;
    productName: string;
    price: number;
    quantity: number;
    pictureUrl: string;
    brand: string;
    type: string;
}

export class CustomerBasket implements Basket {
    basketId: string = uuidv4();
    basketItems: BasketItem[] = [];
}

export interface BasketTotals {
    subtotal: number;
    shipping: number;
    total: number;
}