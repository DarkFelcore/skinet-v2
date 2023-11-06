export interface Pagination<T> {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: Array<T>
}

export class Pagination<T> implements Pagination<T> {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: T[] = [];
}