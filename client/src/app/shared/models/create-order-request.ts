import { Address } from "./common/address";
import { OrderAddress } from "./common/order-address";

export interface CreateOrderRequest {
    basketId: string;
    deliveryMethodId: string;
    address: OrderAddress;
}