import { Component, OnInit } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../auth.service';
import { Router } from '@angular/router';
import { EMAIL_REGEX, PASSWORD_REGEX } from 'src/app/constants/constants';
import { RegisterRequest } from 'src/app/shared/models/register-request';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, map, of, switchMap, timer } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  errors: string[] = [];

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      firstName: [null, [Validators.required, Validators.minLength(3)]],
      lastName: [null, [Validators.required, Validators.minLength(3)]],
      email: [null, [Validators.required, Validators.pattern(EMAIL_REGEX)], [this.validateEmailNotTaken()]],
      password: [null, [Validators.required, Validators.pattern(PASSWORD_REGEX)]]
    })
  }
  
  onSubmit() {
    const request : RegisterRequest = { ...this.registerForm.value };
    this.authService.register(request).subscribe({
      next: () => this.router.navigateByUrl('/shop'),
      error: (err: HttpErrorResponse) => this.errors = Object.values(err.error.errors)
    })
  }
  
  private validateEmailNotTaken(): AsyncValidatorFn {
    return (control: AbstractControl) => {
      control.setErrors({ 'pending': true });
      return this.authService.checkEmailExists(control.value).pipe(
        map((res: boolean) => {
          return res ? { emailExists: true } : null;
        }),
        catchError(() => {
          return of(null);
        })
      );
    };
  }
  
  
}
