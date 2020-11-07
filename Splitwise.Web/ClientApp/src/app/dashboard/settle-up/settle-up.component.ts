import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-settle-up',
  templateUrl: './settle-up.component.html',
  styleUrls: ['./settle-up.component.css']
})
export class SettleUpComponent implements OnInit {

  payee:string;
  payer:string;
  amount:Number;
  constructor() { }

  ngOnInit(): void {
  }
  onSubmit(form:NgForm){
    console.log(form.value);
  }

}
