import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Splitwise } from 'src/app/api/SplitWiseApi';

@Component({
  selector: 'app-expense-edit',
  templateUrl: './expense-edit.component.html',
  styleUrls: ['./expense-edit.component.css'],
})
export class ExpenseEditComponent implements OnInit {
  expenseForm: FormGroup;
  id: string = localStorage.getItem('id');
  group: Splitwise.GroupReturn[] = [];
  expense: Splitwise.AddExpense;
  ID: number;
  constructor(
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private groupService: Splitwise.GroupClient,
    private expenseServie: Splitwise.ExpenseClient,
    private route: Router
  ) {}

  get paidBy() {
    return <FormArray>this.expenseForm.get('paidBy');
  }
  get shares() {
    return <FormArray>this.expenseForm.get('shares');
  }
  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe((params) => {
      this.ID = +params.get('id');
      this.getExpenseBasedOnId(this.ID);
    });
    this.expenseForm = this.fb.group({
      expenseId: 0,
      shares: this.fb.array([this.AddShares()]),
      description: '',
      date: '',
      groupId: null,
      createdBy: this.id,
      splitType: '',
      amount: null,
      paidBy: this.fb.array([this.Addmember()]),
      isSettled: false,
      isDeleted: false,
    });
    this.groupService.groupList().subscribe((data) => {
      this.group = data;
    });
  }
  getExpenseBasedOnId(id: number) {
    this.expenseServie
      .getExpenseBasedOnId(id)
      .subscribe((data: Splitwise.AddExpense) => {
        this.expenseForm = this.fb.group({
          expenseId: data.expenseId,
          shares: this.fb.array(data.shares.map(datum => this.SetShares(datum))),
          description: data.description,
          date: data.date,
          groupId: data.groupId,
          createdBy: data.createdBy,
          splitType: data.splitType,
          amount: data.amount,
          paidBy: this.fb.array(data.paidBy.map(datum => this.SetPaidBy(datum))),
          isSettled: data.isSettled,
          isDeleted: data.isDeleted,
        });
      });
  }
  SetShares(data){
    return this.fb.group({
      email: this.fb.control(data.email),
      name: this.fb.control(data.name),
      share_Percentage: this.fb.control(data.share_Percentage),
      share_Amount: this.fb.control(data.share_Amount),
    });
  }
  SetPaidBy(data){
    return this.fb.group({
      email: this.fb.control(data.email),
      name: this.fb.control(data.name),
      paid_Amount: this.fb.control(data.paid_Amount)
    });
  }
  addShareMember() {
    this.shares.push(this.AddShares());
  }
  removeShareMember(index: number) {
    this.shares.removeAt(index);
  }
  addpaidBy() {
    this.paidBy.push(this.Addmember());
  }
  removepaidBy(index: number) {
    this.paidBy.removeAt(index);
  }
  Addmember(): FormGroup {
    return this.fb.group({
      name: '',
      email: '',
      paid_Amount: 0,
    });
  }
  AddShares(): FormGroup {
    return this.fb.group({
      name: '',
      email: '',
      share_Percentage: 0,
      share_Amount: 0,
    });
  }
  onSubmit() {
    this.expense = this.expenseForm.value;
    console.log(this.expense)
    // this.expenseServie.postExpense(this.expense).subscribe(() => {
    //   console.log('Expense Added!');
    //   this.route.navigate(['/dashboard/splitwise']);
    // });
  }
}
