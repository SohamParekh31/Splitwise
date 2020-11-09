import { Component, OnInit } from '@angular/core';
import { Splitwise } from '../../api/SplitWiseApi'

@Component({
  selector: 'app-my-expense',
  templateUrl: './my-expense.component.html',
  styleUrls: ['./my-expense.component.css']
})
export class MyExpenseComponent implements OnInit {

  p: number = 1;
  expense:Splitwise.Expense[] = [];

  constructor(private expenseService:Splitwise.ExpenseClient) { }

  ngOnInit(): void {
    this.expenseService.getexpense().subscribe(
      (data) => {
        this.expense = data;
      }
    );
  }
  deleteExpense(expense:Splitwise.Expense){
    if(confirm(`Are you sure,You want to delete Expense ${expense.description} ?`)){
      this.expenseService.deleteExpense(expense.expenseId).subscribe(
        () => {
          console.log("Expense Deleted!");
        }
      );
    }
  }

}
