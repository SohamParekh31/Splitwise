import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Splitwise } from 'src/app/api/SplitWiseApi';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  email:string;
  password:string;
  logindata:Splitwise.Login
  constructor(private loginService: Splitwise.AccountClient,private router:Router) { }

  ngOnInit(): void {
  }

  login(form:NgForm){
    this.logindata = form.value;
    this.loginService.login(this.logindata).subscribe(
      (res:any) => {
        //console.log(res.token);
        localStorage.setItem('token',res.token);
        var token = localStorage.getItem('token');
        const payLoad = JSON.parse(window.atob(token.split('.')[1]));
        localStorage.setItem('id',payLoad['ID']);
        console.log("Login Successfull");
        this.router.navigate(['/dashboard/splitwise']);
      }
    );
  }

}
