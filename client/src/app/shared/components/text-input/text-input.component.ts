import { Component, ElementRef, Input, Self, ViewChild } from '@angular/core';
import { AbstractControl, NgControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent {
  @ViewChild('input', {static : true}) input: ElementRef; // reference to the input field
  @Input() type? : 'email' | 'text' | 'password' = 'text'; // input type (password, text, date, button, ...)
  @Input() label?: string; // Label that describes the input field
  @Input() placeHolder?: string;
  @Input() patternErrorMessage?: string;
  @Input() isDisabled: boolean = false;

  constructor(
    @Self() public controlDir: NgControl
  ) {
    // Bind the valueAccessor to this particular class, which lets us access the control directive inside our component + template
    this.controlDir.valueAccessor = this;
  }

  ngOnInit(): void {
    const control = this.controlDir.control as AbstractControl;
    control.updateValueAndValidity();
  }

  onChange(event? : any) { }

  onTouched(event? : any) {}

  writeValue(obj: any): void {
    this.input.nativeElement.value = obj || '';
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
}
