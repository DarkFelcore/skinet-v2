import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Product } from './shared/models/product';
import { Pagination } from './shared/models/common/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  products: Product[];
  
  constructor(
    private http: HttpClient
  ) { }

  ngOnInit(): void {
    this.http.get<Pagination<Product>>("https://localhost:7075/api/products").subscribe({
      next: (res: Pagination<Product>) => this.products = res.data,
      error: (err: HttpErrorResponse) => console.log(err)
    })

  }

}
