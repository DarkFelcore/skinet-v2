import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/auth/auth.service';
import { Address } from 'src/app/shared/models/common/address';
import { User } from 'src/app/shared/models/common/user';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent implements OnInit {

  @Input() checkoutForm: FormGroup;
  @Input() currentUser: User;

  constructor(
    private authService: AuthService,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
  }

  saveUserAddress(): void {
    const addressForm = this.checkoutForm.get('addressForm')?.value;
    const address: Address = { street: addressForm.street, postalCode: addressForm.postalCode, provice: addressForm.provice, city: addressForm.city, country: addressForm.country };
    this.authService.updateUserAddress(address).subscribe({
      next: (address: Address) => {
        this.checkoutForm.get('addressForm')?.reset({
          ...address,
          firstName: this.currentUser.firstName,
          lastName: this.currentUser.lastName
        });
        this.toastrService.success('Address successfully changed!')
      },
      error: (err: HttpErrorResponse) => {
        console.log(err);
      }
    })
  }

}
