export interface Pagination<T> {
    pageIndex: number;
    pageSize: number;
    data: Array<T>
}