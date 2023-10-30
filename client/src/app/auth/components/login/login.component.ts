import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../auth.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginRequest } from 'src/app/shared/models/login-request';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { EMAIL_REGEX } from 'src/app/constants/constants';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  returnUrl: string;

  constructor(
    private authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // Check auth guard
    // If a user comes from the basket page and click "Proceed to checkout" and is not authenticated, he will be redirected to the login page with "/checkout" as return url.
    // If the user visit the login page, the returnUrl will be '/shop'
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/shop';
    this.createLoginForm();
  }

  createLoginForm(): void {
    this.loginForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.pattern(EMAIL_REGEX)]),
      password: new FormControl(null, Validators.required)
    });
  }

  onSubmit() {
    var request : LoginRequest = { email: this.loginForm.value.email, password: this.loginForm.value.password }
    this.authService.login(request).subscribe({
      next: () => this.router.navigateByUrl(this.returnUrl),
      error: (err: HttpErrorResponse) => console.log(err)
    });
  }

}
