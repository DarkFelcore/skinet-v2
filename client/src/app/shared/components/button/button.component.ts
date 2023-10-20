import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss']
})

export class ButtonComponent {
  @Input() label: string = '';
  @Input() type: 'submit' | 'button' = 'button';
  @Input() color: 'primary' | 'secondary' = 'primary';

  getClass(): string {
    return `btn btn-outline-${this.color}`;
  }
}
