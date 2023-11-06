import { EMPTY_GUID } from "../constants/constants";

export class ShopParams {
    brandId = EMPTY_GUID;
    typeId = EMPTY_GUID;
    sort = 'name';
    pageNumber = 1;
    pageSize = 6;
    search: string;
}