import { ProductItemOrdered } from "./product-item-ordered";

export interface OrderItem {
    orderItemId: string;
    itemOrdered: ProductItemOrdered;
    price: number;
    quantity: number;
}