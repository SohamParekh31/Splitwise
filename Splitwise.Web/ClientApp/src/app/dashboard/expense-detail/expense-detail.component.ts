import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Splitwise } from 'src/app/api/SplitWiseApi';

@Component({
  selector: 'app-expense-detail',
  templateUrl: './expense-detail.component.html',
  styleUrls: ['./expense-detail.component.css']
})
export class ExpenseDetailComponent implements OnInit {

  ID:number;
  expense:Splitwise.AddExpense;
  constructor(private activatedRoute:ActivatedRoute,private expenseService:Splitwise.ExpenseClient) { }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(
      params => {
        this.ID = +params.get('id');
        this.getExpenseById(this.ID);
      }
    );
  }

  getExpenseById(id){
    this.expenseService.getExpenseBasedOnId(id).subscribe(
      (data) => {
        this.expense = data
      }
    )
  }

}
