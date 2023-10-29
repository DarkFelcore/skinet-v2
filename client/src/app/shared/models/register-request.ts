import { Address } from "./common/address";

export interface RegisterRequest {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    address: Address;
}