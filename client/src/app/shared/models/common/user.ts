import { Address } from "./address";

export interface User {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    token: string;
    address: Address;
}