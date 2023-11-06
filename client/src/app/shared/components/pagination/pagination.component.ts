import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnInit {
  @Input() pageNumber: number;
  @Input() pageSize: number;
  @Input() totalCount: number;

  @Output() onPageChangedEmitter: EventEmitter<number> = new EventEmitter<number>();

  ngOnInit(): void {}

  onPageChanged(event: any) {
    this.onPageChangedEmitter.emit(event.page);
  }
  
}
