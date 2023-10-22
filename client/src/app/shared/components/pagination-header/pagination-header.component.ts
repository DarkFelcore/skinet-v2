import { Component, Input } from '@angular/core';
import { Product } from '../../models/common/product';

@Component({
  selector: 'app-pagination-header',
  templateUrl: './pagination-header.component.html',
  styleUrls: ['./pagination-header.component.scss']
})
export class PaginationHeaderComponent {
  @Input() totalCount : number;
  @Input() pageSize : number;
  @Input() pageIndex : number;
  @Input() products: Product[] | undefined
}
