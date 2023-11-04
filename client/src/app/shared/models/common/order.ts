import { DeliveryMethod } from "./delivery-method";
import { OrderAddress } from "./order-address";
import { OrderItem } from "./order-item";

export interface Order {
    orderId: string;
    orderItems: OrderItem[];
    buyerEmail: string;
    orderDate: string;
    shipToAddress: OrderAddress,
    deliveryMethod: DeliveryMethod;
    subtotal: number;
    total: number;
    status: string;
    paymentIntentId: string;
}