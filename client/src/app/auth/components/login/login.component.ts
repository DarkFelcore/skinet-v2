import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../auth.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginRequest } from 'src/app/shared/models/login-request';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.createLoginForm();
  }

  createLoginForm(): void {
    this.loginForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.pattern(/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/)]),
      password: new FormControl(null, [Validators.required, Validators.minLength(4)])
    });
  }

  onSubmit() {
    var request : LoginRequest = { email: this.loginForm.value.email, password: this.loginForm.value.password }
    this.authService.login(request).subscribe({
      next: () => this.router.navigateByUrl('/shop'),
      error: (err: HttpErrorResponse) => console.log(err)
    });
  }

}
