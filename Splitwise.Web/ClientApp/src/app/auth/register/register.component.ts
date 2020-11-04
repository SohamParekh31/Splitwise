import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Splitwise } from 'src/app/api/SplitWiseApi';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  email:string;
  password:string;
  phoneNumber:string;
  firstName:string;
  lastName:string;
  currency:string;
  register:Splitwise.Register;
  constructor(private registerService: Splitwise.AccountClient,private router:Router) { }

  ngOnInit(): void {
  }
  registerForm(form:NgForm){
    this.register = form.value;
    console.log(this.register);
    this.registerService.register(this.register).subscribe(
      () => {
        console.log("Registration Successfull!");
        this.router.navigate(['/dashboard/splitwise']);
      }
    )
  }

}
