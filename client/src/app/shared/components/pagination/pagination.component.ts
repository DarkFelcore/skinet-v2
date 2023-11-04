import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PaginationService } from 'src/app/shared/services/pagination.service';
import { Product } from '../../models/common/product';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnInit {
  @Input() pageIndex: number | undefined;
  @Input() pageSize: number | undefined;
  @Input() totalCount: number | undefined;
  @Input() products: Product | undefined
  
  currentPage: number = 1;
  totalPages: number[];

  constructor(
    private paginationService: PaginationService
  ) {}

  ngOnInit(): void {
    this.paginationService.currentPage$.subscribe((page) => {
      this.currentPage = page;
    });
  }

  ngOnChanges(): void {
    this.generatePageRange();
  }

  generatePageRange(): void {
    const pageCount = Math.ceil((this.totalCount ?? 0) / (this.pageSize ?? 6));


    const pageRange: number[] = [];
    
    for (let i = 1; i <= pageCount; i++) {
      pageRange.push(i);
    }
    
    this.totalPages = pageRange;
  }

  onPageChanged(page: number) {
    this.paginationService.setCurrentPage(page);
  }
  
}
