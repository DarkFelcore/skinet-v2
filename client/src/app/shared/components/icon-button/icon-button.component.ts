import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-icon-button',
  templateUrl: './icon-button.component.html',
  styleUrls: ['./icon-button.component.scss']
})
export class IconButtonComponent implements OnInit {
  @Input() className: string;
  @Input() icon: string;
  @Input() label?: string;
  @Input() iconPosition?: 'left' | 'right' = 'right';
  @Input() step?: 'next' | 'previous';
  @Input() disabled?: boolean = false;

  ngOnInit(): void {
  }
}
