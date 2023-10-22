import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent {
  @Input() type: 'text' | 'password' | 'email' = 'text';
  @Input() width: string;
  @Input() class: string = 'form-control';
  @Input() placeholder: string;

  value: string;

  getStyle() {
    return this.width;
  }
}
