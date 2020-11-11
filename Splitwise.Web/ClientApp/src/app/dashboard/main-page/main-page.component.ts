import { Component, OnInit } from '@angular/core';
import { Splitwise } from 'src/app/api/SplitWiseApi';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {

  payee:string;
  payer:string;
  amount:Number;
  groupId:Number;
  settleUp:Splitwise.SettleUp;
  friend:Splitwise.Friend[] = [];
  userModel:Splitwise.UserModel;
  balance;
  constructor(private friendService:Splitwise.FriendClient,
            private accountService:Splitwise.AccountClient,
            private expenseService:Splitwise.ExpenseClient) { }

  ngOnInit(): void {
    var result = 0.0;
    this.accountService.getUserInfo().subscribe(
      (data:Splitwise.UserModel) => {
        this.userModel = data;
        this.getBalance(data);
        // data.owesfrom.forEach(s => {
        //   result = s.amount+result;
        // })
        // console.log(result);
      }
    );
    this.friendService.getFriendList().subscribe(
      (data) => {
        this.friend = data
      }
    );
  }
  getBalance(model:Splitwise.UserModel){
    var resultOwesFrom = 0.0;
    var resultOwesTo = 0.0;
    model.owesfrom.forEach(s => {
      resultOwesFrom = s?.amount + resultOwesFrom;
    });
    model.owsto.forEach(s => {
      resultOwesTo = s?.amount + resultOwesTo;
    });
    this.balance = resultOwesFrom - resultOwesTo;
  }

  onSubmit(form){
    this.settleUp = form.value;
    this.expenseService.settlment(this.settleUp).subscribe(
      () => {
        console.log("Settlement Success!");
        location.reload();
      },
      () => {
        alert("No Amount to Pay");
      }
    );
  }
}
