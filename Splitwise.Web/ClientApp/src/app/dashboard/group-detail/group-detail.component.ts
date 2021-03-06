import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Splitwise } from '../../api/SplitWiseApi'


@Component({
  selector: 'app-group-detail',
  templateUrl: './group-detail.component.html',
  styleUrls: ['./group-detail.component.css']
})
export class GroupDetailComponent implements OnInit {
  p:number = 1;
  ID:number;
  groupDetails:Splitwise.AddGroup;
  groupExpenseList:Splitwise.Expense[] = [];
  payee:string;
  payer:string;
  amount:Number;
  groupId:Number;
  settleUp:Splitwise.SettleUp;
  userId = localStorage.getItem('id');
  constructor(private groupService:Splitwise.GroupClient,private activatedRoute:ActivatedRoute,private expenseService:Splitwise.ExpenseClient) { }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(
      params => {
        this.ID = +params.get('id');
        this.getGroupById(this.ID);
      }
    )
    this.groupService.getGroupExpenseList(this.ID).subscribe(
      (data) => {
        this.groupExpenseList = data;
      }
    );
  }
  getGroupById(id){
    this.groupService.getGroupBasedOnId(id).subscribe(
      (data) => {
        this.groupDetails = data;
        this.groupId = this.groupDetails?.groupId;
      }
    );
  }
  deleteExpense(expense){
    if(confirm(`Are you sure,You want to delete Expense ${expense.description} ?`)){
      this.expenseService.deleteExpense(expense.expenseId).subscribe(
        () => {
          console.log("Expense Deleted!");
        }
      );
    }
  }
  onSubmit(form){
    this.settleUp = form.value;
    this.expenseService.settlment(this.settleUp).subscribe(
      () => {
        console.log("Settlement Success!");
        location.reload();
      }
    );
  }
}
