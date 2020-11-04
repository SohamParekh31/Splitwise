import { Component, OnInit } from '@angular/core';
import { Splitwise } from '../../api/SplitWiseApi'

@Component({
  selector: 'app-my-expense',
  templateUrl: './my-expense.component.html',
  styleUrls: ['./my-expense.component.css']
})
export class MyExpenseComponent implements OnInit {

  expense:Splitwise.Expense[] = [];
  constructor(private expenseService:Splitwise.ExpenseClient) { }

  ngOnInit(): void {
    this.expenseService.getexpense().subscribe(
      (data) => {
        this.expense = data;
        console.log(this.expense);
      }
    );
  }

}
