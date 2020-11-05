import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { Splitwise } from 'src/app/api/SplitWiseApi';


@Component({
  selector: 'app-add-expense',
  templateUrl: './add-expense.component.html',
  styleUrls: ['./add-expense.component.css']
})
export class AddExpenseComponent implements OnInit {

  expenseForm: FormGroup;
  id:string = localStorage.getItem('id');
  group:Splitwise.GroupReturn[] = [];
  expense:Splitwise.AddExpense;
  constructor(private fb: FormBuilder,private groupService:Splitwise.GroupClient,private expenseServie:Splitwise.ExpenseClient) { }

  get paidBy(){
    return <FormArray>this.expenseForm.get('paidBy');
  }
  get shares(){
    return <FormArray>this.expenseForm.get('shares');
  }
  ngOnInit(): void {
    this.expenseForm = this.fb.group({
      shares:this.fb.array([this.AddShares()]),
      description:'',
      date:'',
      groupId:null,
      createdBy:this.id,
      splitType:'',
      amount:null,
      paidBy:this.fb.array([this.Addmember()]),
      isSettled:false,
      isDeleted:false,
    });
    this.groupService.groupList().subscribe(
      (data) => {
        this.group = data;
      }
    );
  }
  getSplitTypeValue(){
    var value = this.expenseForm.value;
    return value.splitType;
  }
  addShareMember(){
    this.shares.push(this.AddShares());
  }
  removeShareMember(index:number){
    this.shares.removeAt(index);
  }
  addpaidBy(){
    this.paidBy.push(this.Addmember());
  }
  removepaidBy(index:number){
    this.paidBy.removeAt(index);
  }
  Addmember(): FormGroup{
    return this.fb.group({
      name:'',
      email:'',
      paid_Amount:0
    })
  }
  AddShares(): FormGroup{
    return this.fb.group({
      name:'',
      email:'',
      share_Percentage:0,
      share_Amount:0
    })
  }
  onSubmit(){
    this.expense = this.expenseForm.value;
    console.log(this.expense);
    this.expenseServie.postExpense(this.expense).subscribe(
      () => {
        console.log("Expense Added!");
      }
    );
  }

}
