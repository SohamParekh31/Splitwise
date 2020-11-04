import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';


@Component({
  selector: 'app-add-expense',
  templateUrl: './add-expense.component.html',
  styleUrls: ['./add-expense.component.css']
})
export class AddExpenseComponent implements OnInit {

  expenseForm: FormGroup;

  constructor(private fb: FormBuilder) { }

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
      createdBy:'',
      splitType:'',
      amount:null,
      paidBy:this.fb.array([this.Addmember()]),
      isSettled:false,
      isDeleted:false,
    });
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
      paid_Amount:null
    })
  }
  AddShares(): FormGroup{
    return this.fb.group({
      name:'',
      email:'',
      share_Percentage:null,
      share_Amount:null
    })
  }
  onSubmit(){
    console.log(this.expenseForm.value);
  }

}
